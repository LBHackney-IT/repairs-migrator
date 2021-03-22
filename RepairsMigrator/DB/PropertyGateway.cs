using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{

    public class PropertyGateway
    {
        
        public async Task<Dictionary<string, PropRefModel>> GetPropertyReferences(IEnumerable<string> addresses)
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            Dictionary<string, PropRefModel> result = new Dictionary<string, PropRefModel>();
            await using (var cmd = new NpgsqlCommand("select address, prop_ref, short_address from dbo.property_matching", conn))
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

        public async Task<IEnumerable<HierarchyModel>> GetHierarchyDetails(IEnumerable<string> references)
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
                v.level_code
	            from dbo.vw_property_hierarchy v
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
        public string LevelCode { get; set; }
    }
}
