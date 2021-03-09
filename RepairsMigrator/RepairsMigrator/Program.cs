using Core;
using CSV;
using System;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            W("Starting");
            W("Loading CSV");
            var dloIn = CSVLoader.LoadCsv<DLOSheet>("Resources/RepairsOrdersPostCA.csv");
            W("Building Pipeline");
            var pipeline = BuildPipeline();

            W("Running Pipeline");
            var outPut = await pipeline.Run(dloIn);

            W("Writing Output");
            CSVSaver.SaveCsv("out.csv", outPut);
            W("Finished");
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
