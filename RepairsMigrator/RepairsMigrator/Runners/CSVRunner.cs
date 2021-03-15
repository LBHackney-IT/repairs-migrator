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
        public static async Task ProcessCSVs(Pipeline<TargetOutputSheet> pipeline)
        {
            var output = new List<TargetOutputSheet>();

            var types = GoogleRunner.TypeMaps;

            foreach (var type in types)
            {
                output.AddRange(await pipeline.Run(LoadCSV(type.Key, type.Value), type.Value));
            }

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

        private static async Task<IEnumerable<TargetOutputSheet>> LoadAndRun<TIn>(string path, Pipeline<TargetOutputSheet> pipeline)
            where TIn : class, new()
        {
            var dloIn = CSVLoader.LoadCsv<TIn>(path);

            return await pipeline.Run(dloIn);
        }
    }
}
