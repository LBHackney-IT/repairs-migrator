using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{

    public class PropertyGateway
    {
        
        public async Task<Dictionary<string, string>> GetPropertyReferences(IEnumerable<string> addresses)
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            var (tableName, columnName) = await WriteValues(addresses, conn);

            Dictionary<string, string> result = new Dictionary<string, string>();
            await using (var cmd = new NpgsqlCommand($"select {columnName}, 'test' from {tableName}", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(reader.GetString(0), reader.GetString(1));
                }
            }

            return result;
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
            return new List<HierarchyModel>();
        }
    }

    public class HierarchyModel
    {
        public string PropReference { get; set; }
        public string PropAddress { get; set; }
        public string SubblockReference { get; set; }
        public string SubblockAddress { get; set; }
        public string BlockReferece { get; set; }
        public string BlockAdress { get; set; }
        public string EstateReference { get; set; }
        public string EstateAddress { get; set; }
    }
}
