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
        private static readonly Dictionary<string, Type> typeMaps = new Dictionary<string, Type>
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

        private static async Task<IList<GoogleSheetResult>> LoadSheetData()
        {
            var sm = new SheetManager("RepairsMigration", GoogleCredential.FromFile("Resources/creds.json"));
            var pointerSheet = await sm.LoadSheet<DocumentHub>("1Fg_xi24p0YS-ZI1UDB3lCMT7phZSe5nJLu4UJYKF9Is", "Main");

            var result = new List<GoogleSheetResult>();

            int attemptedLoads = 0;
            foreach (var item in pointerSheet)
            {
                string contractor = item.Contractor;
                if (!string.IsNullOrWhiteSpace(item.Sheet)
                    && typeMaps.TryGetValue(contractor, out var type))
                {
                    attemptedLoads++;
                    Log.Information($"Loading {contractor} from {item.DocumentId}:{item.Sheet}");
                    IEnumerable<object> data = await sm.LoadSheet(item.DocumentId, item.Sheet, type);
                    AttachContractor(data, contractor);
                    result.Add(new GoogleSheetResult(type, data));
                }
            }

            Log.Information("Attempted Loads: {attemptedLoads}", attemptedLoads);

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

        public GoogleSheetResult(Type modelType, IEnumerable<object> data)
        {
            ModelType = modelType;
            Data = data;
        }
    }
}
