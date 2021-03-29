using Npgsql;
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
