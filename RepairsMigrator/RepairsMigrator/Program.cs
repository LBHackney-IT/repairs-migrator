using Core;
using CSV;
using RepairsMigrator.SheetModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            W("Starting");
            var output = new List<TargetOutputSheet>();

            output.AddRange(await LoadAndRun<DLOSheet>("Resources/DLO.csv"));
            output.AddRange(await LoadAndRun<AxisSheet>("Resources/Axis.csv"));
            output.AddRange(await LoadAndRun<AvonlineSheet>("Resources/Avonline.csv"));
            output.AddRange(await LoadAndRun<AlphatrackSheet>("Resources/Alphatrack.csv"));
            output.AddRange(await LoadAndRun<HertsSheet>("Resources/Herts.csv"));
            output.AddRange(await LoadAndRun<PurdySheet>("Resources/Purdy.csv"));
            output.AddRange(await LoadAndRun<StannahSheet>("Resources/Stannah.csv"));

            W("Writing Output");
            CSVSaver.SaveCsv("out.csv", output);
            W("Finished");
            Console.ReadLine();
        }

        private static async Task<IEnumerable<TargetOutputSheet>> LoadAndRun<TIn>(string path)
            where TIn : class, new()
        {
            W($"Loading CSV: {path}");
            var dloIn = CSVLoader.LoadCsv<TIn>(path);
            W("Building Pipeline");
            var pipeline = BuildPipeline();

            W("Running Pipeline");
            return await pipeline.RunBatch(dloIn);
        }

        private static Pipeline<TargetOutputSheet> BuildPipeline()
        {
            return new PipelineBuilder().Build<TargetOutputSheet>();
        }

        private static void W(string message)
        {
            Console.WriteLine(message);
        }
    }
}
