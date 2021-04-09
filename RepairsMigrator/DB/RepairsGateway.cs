using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class RepairsGateway
    {
        public static async Task<IList<RepairsHubDBModel>> GetRepairsData()
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            List<RepairsHubDBModel> result = new List<RepairsHubDBModel>();
            await using (var cmd = new NpgsqlCommand($@"
                SELECT 
                    id, 
                    description_of_work, 
                    date_raised, 
                    est_cost, 
                    prop_reference, 
                    completion_time, 
                    trade_name, 
                    priority_desc, 
                    contractor
	            FROM migration.migration_input", conn))

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(new RepairsHubDBModel
                    {
                        Id = reader.GetValue(0) as string,
                        Description = reader.GetValue(1) as string,
                        DateRaised = reader.GetValue(2) as string,
                        Cost = reader.GetValue(3) as string,
                        PropRef = reader.GetValue(4) as string,
                        Completion_Time = reader.GetValue(5) as string,
                        TradeName = reader.GetValue(6) as string,
                        PriorityDescription = reader.GetValue(7) as string,
                        Contractor = reader.GetValue(8) as string,
                    });
                }
            }

            return result;
        }

        public static async Task StoreRepairsData(IEnumerable newData)
        {
            var connectionString = Configuration.Instance["ConnectionString"];

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await SetupTable(conn);
            SendRepairsData(conn, newData);
        }

        private static void SendRepairsData(NpgsqlConnection conn, IEnumerable values)
        {
            using (var writer = conn.BeginBinaryImport(@"
                                                        COPY migration.interrim_data (
                                                                            description,
                                                                            trade_code,
                                                                            priority_code,
                                                                            property_reference,
                                                                            contractor_reference,
                                                                            date_raised,
                                                                            date_completed,
                                                                            completion_description,
                                                                            appointment_date,
                                                                            appointment_time,
                                                                            sor_code
                                                        ) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var value in values)
                {
                    writer.StartRow();
                    writer.Write(value);
                }

                writer.Complete();
            }
        }

        private static async Task SetupTable(NpgsqlConnection conn)
        {
            await using (var cmd = new NpgsqlCommand(@"
                CREATE TABLE IF NOT EXISTS migration.interrim_data (
                    description text,
                    trade_code text,
                    priority_code text,
                    property_reference text,
                    contractor_reference text,
                    date_raised text,
                    date_completed text,
                    completion_description text,
                    appointment_date text,
                    appointment_time text,
                    sor_code json
                );
                ", conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }

            await using (var cmd = new NpgsqlCommand(@"TRUNCATE migration.interrim_data", conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public class RepairsHubDBModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string DateRaised { get; set; }
        public string Cost { get; set; }
        public string PropRef { get; set; }
        public string Completion_Time { get; set; }
        public string TradeName { get; set; }
        public string PriorityDescription { get; set; }
        public string Contractor { get; set; }
    }
}
