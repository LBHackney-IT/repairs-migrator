using Core;
using CSV;
using RepairsMigrator.SheetModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    class CSVRunner
    {
        public static async Task ProcessCSVs(Pipeline pipeline)
        {
            var types = GoogleRunner.TypeMaps;

            foreach (var type in types)
            {
                pipeline.In(LoadCSV(type.Key, type.Value), type.Value);
            }

            await pipeline.Run();

            var output = pipeline.Out<TargetOutputSheet>();

            CSVSaver.SaveCsv("out_csv.csv", output);
        }

        private static IEnumerable<object> LoadCSV(string name, Type type)
        {
            string fileName = $"csvs/{CSVLoader.FixFileName(name)}.csv";
            if (!File.Exists(fileName))
            {
                Log.Information($"Failed to find {fileName} so skipping");
                return new List<object>();
            }

            Log.Information($"Loading {fileName} to type {type.Name}");
            return CSVLoader.LoadCsv(fileName, type);
        }

        public static async Task LoadSheetsForTest()
        {
            if (!Directory.Exists("csvs"))
            {
                Directory.CreateDirectory("csvs");
            }

            if (Directory.GetFiles("csvs").Length > 0)
            {
                Log.Information("Found existing csvs folder. Skipping load from google");
                return;
            }

            Log.Information("Loading Snapshot from google");
            var data = await GoogleRunner.LoadSheetData();

            foreach (var item in data)
            {
                string fileName = CSVLoader.FixFileName(item.Contractor);
                Log.Information($"Saving csvs/{fileName}.csv");
                CSVSaver.SaveCsv($"csvs/{fileName}.csv", item.Data, item.ModelType);
            }
        }
    }
}
