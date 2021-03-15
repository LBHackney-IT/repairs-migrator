using Core;
using CSV;
using Google;
using Google.Apis.Auth.OAuth2;
using RepairsMigrator.SheetModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    class GoogleRunner
    {
        public static readonly Dictionary<string, Type> TypeMaps = new Dictionary<string, Type>
        {
            { "DLO", typeof(DLOSheet) },
            { "Avonline", typeof(AvonlineSheet) },
            { "Axis", typeof(AxisSheet) },
            { "Alphatrack", typeof(AlphatrackSheet) },
            { "Purdy", typeof(PurdySheet) },
            { "Stannah", typeof(StannahSheet) },
            { "Herts Heritage", typeof(HertsSheet) },
            { "Door Entry", typeof(CMSheet) },
            { "Lightning Protection", typeof(CMSheet) },
            { "Lift Breakdown", typeof(CMSheet) },
            { "Fire Alarm/AOV", typeof(CMSheet) },
            { "Electrical Supplies", typeof(CMSheet) },
            { "Electric Heating", typeof(CMSheet) },
            { "Communal Lighting", typeof(CMSheet) },
            { "Emergency Lighting Servicing", typeof(CMSheet) },
            { "Reactive Rewires", typeof(CMSheet) },
            { "FRA Works", typeof(CMSheet) },
            { "Lift Servicing", typeof(CMSheet) },
            { "Heat Meters", typeof(CMSheet) },
            { "T.V Aerials", typeof(CMSheet) },
            { "DPA", typeof(CMSheet) },
            { "Mech. Repairs", typeof(CMSheet) },
            { "CP12", typeof(CMSheet) },
        };


        public static async Task ProcessGoogleSheets(Pipeline<TargetOutputSheet> pipeline)
        {
            var allSheets = await LoadSheetData();

            var output = new List<TargetOutputSheet>();

            foreach (var sheet in allSheets)
            {
                output.AddRange(await pipeline.Run(sheet.Data, sheet.ModelType));
            }

            CSVSaver.SaveCsv("out_google.csv", output);
        }

        public static async Task<IList<GoogleSheetResult>> LoadSheetData()
        {
            var sm = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            var pointerSheet = await sm.LoadSheet<DocumentHub>("1Fg_xi24p0YS-ZI1UDB3lCMT7phZSe5nJLu4UJYKF9Is", "Main");

            var result = new List<GoogleSheetResult>();

            int attemptedLoads = 0;
            int successfulLoads = 0;
            foreach (var item in pointerSheet)
            {
                string contractor = item.Contractor;
                if (!string.IsNullOrWhiteSpace(item.Sheet)
                    && TypeMaps.TryGetValue(contractor, out var type))
                {
                    attemptedLoads++;
                    Log.Information($"Loading {contractor} from {item.DocumentId}:{item.Sheet}");
                    try
                    {
                        IEnumerable<object> data = await sm.LoadSheet(item.DocumentId, item.Sheet, type);
                        AttachContractor(data, contractor);
                        result.Add(new GoogleSheetResult(type, data, contractor));
                        successfulLoads++;
                    } catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                }
            }

            Log.Information("Attempted Loads: {attemptedLoads}", attemptedLoads);
            Log.Information("Successful Loads: {successfulLoads}", successfulLoads);

            return result;
        }

        private static void AttachContractor(IEnumerable<object> data, string contractor)
        {
            foreach (var row in data)
            {
                if (row is IAppColumns withAppColumns)
                {
                    withAppColumns.Contractor = contractor;
                }
            }
        }
    }

    internal class GoogleSheetResult
    {
        public Type ModelType;
        public IEnumerable<object> Data;
        public readonly string Contractor;

        public GoogleSheetResult(Type modelType, IEnumerable<object> data, string contractor)
        {
            ModelType = modelType;
            Data = data;
            this.Contractor = contractor;
        }
    }
}
