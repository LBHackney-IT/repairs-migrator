using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{

    public class PropertyGateway
    {
        
        public static async Task<Dictionary<string, PropRefModel>> GetPropertyReferences()
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            Dictionary<string, PropRefModel> result = new Dictionary<string, PropRefModel>();
            await using (var cmd = new NpgsqlCommand("select address, prop_ref, short_address from migration.property_matching", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(reader.GetString(0), new PropRefModel
                    {
                        PropRef = reader.GetString(1),
                        ResolvedAddress = reader.GetString(2)
                    });
                }
            }

            return result;
        }

        public static async Task LoadAddressStore(IEnumerable<string> addresses)
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("TRUNCATE migration.address_store", conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }

            using (var writer = conn.BeginBinaryImport("COPY migration.address_store (address) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var value in addresses)
                {
                    writer.StartRow();
                    writer.Write(value);
                }

                writer.Complete();
            }

            await using (var cmd = new NpgsqlCommand("CALL migration.update_address_lookup()", conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public class PropRefModel
        {
            public string PropRef { get; set; }
            public string ResolvedAddress { get; set; }
        }

        private static async Task<(string tablename, string columnName)> WriteValues(IEnumerable<string> values, NpgsqlConnection conn)
        {
            await using (var cmd = new NpgsqlCommand(@"CREATE TEMPORARY TABLE tmp_store (
                                                           cValue text
                                                        ); ", conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }

            using (var writer = conn.BeginBinaryImport("COPY tmp_store (cValue) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var value in values)
                {
                    writer.StartRow();
                    writer.Write(value);
                }

                writer.Complete();
            }

            return ("tmp_store", "cValue");
        }

        public static async Task<IEnumerable<HierarchyModel>> GetHierarchyDetails(IEnumerable<string> references)
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            var (tableName, columnName) = await WriteValues(references, conn);

            List<HierarchyModel> result = new List<HierarchyModel>();
            await using (var cmd = new NpgsqlCommand($@"
            select 
	            v.prop_ref, 
	            v.short_address, 
	            v.sub_block_ref, 
	            v.sub_block_address,
	            v.block_ref,
	            v.block_address,
	            v.estate_ref,
	            v.estate_address,
                v.level_code,
                v.owner,
                v.owner_address,
                v.lu_desc,
                v.cat_type,
                v.rep_area
	            from migration.vw_property_hierarchy v
            INNER JOIN {tableName} t ON t.{columnName} = v.prop_ref
            ", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(new HierarchyModel
                    {
                        PropReference = reader.GetValue(0) as string,
                        PropAddress = reader.GetValue(1) as string,
                        SubblockReference = reader.GetValue(2) as string,
                        SubblockAddress = reader.GetValue(3) as string,
                        BlockReference = reader.GetValue(4) as string,
                        BlockAddress = reader.GetValue(5) as string,
                        EstateReference = reader.GetValue(6) as string,
                        EstateAddress = reader.GetValue(7) as string,
                        LevelCode = reader.GetValue(8) as string,
                        OwnerReference = reader.GetValue(9) as string,
                        OwnerAddress = reader.GetValue(10) as string,
                        Level_Description = reader.GetValue(11) as string,
                        Category_Type = reader.GetValue(12) as string,
                        Rep_Area = reader.GetValue(13) as string,
                    });
                }
            }

            return result;
        }
    }

    public class HierarchyModel
    {
        public string PropReference { get; set; }
        public string PropAddress { get; set; }
        public string SubblockReference { get; set; }
        public string SubblockAddress { get; set; }
        public string BlockReference { get; set; }
        public string BlockAddress { get; set; }
        public string EstateReference { get; set; }
        public string EstateAddress { get; set; }
        public string OwnerReference { get; set; }
        public string OwnerAddress { get; set; }
        public string LevelCode { get; set; }
        public string Rep_Area { get; set; }
        public string Level_Description { get; set; }
        public string Category_Type { get; set; }
    }
}
