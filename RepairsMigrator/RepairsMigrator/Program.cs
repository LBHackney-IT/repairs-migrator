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
            var outPut = new List<TargetOutputSheet>();

            outPut.AddRange(await LoadAndRun<DLOSheet>("Resources/DLO.csv"));
            outPut.AddRange(await LoadAndRun<AxisSheet>("Resources/Axis.csv"));
            outPut.AddRange(await LoadAndRun<AvonlineSheet>("Resources/Avonline.csv"));
            outPut.AddRange(await LoadAndRun<AlphatrackSheet>("Resources/Alphatrack.csv"));
            outPut.AddRange(await LoadAndRun<HertsSheet>("Resources/Herts.csv"));
            outPut.AddRange(await LoadAndRun<PurdySheet>("Resources/Purdy.csv"));
            outPut.AddRange(await LoadAndRun<StannahSheet>("Resources/Stannah.csv"));

            W("Writing Output");
            CSVSaver.SaveCsv("out.csv", outPut);
            W("Finished");
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
