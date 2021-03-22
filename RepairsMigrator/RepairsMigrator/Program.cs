using Core;
using CSV;
using RepairsMigrator.Runners;
using RepairsMigrator.Stages;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine(
@"
===========================================
  _______ _            _______          _ 
 |__   __| |          |__   __|        | |
    | |  | |__   ___     | | ___   ___ | |
    | |  | '_ \ / _ \    | |/ _ \ / _ \| |
    | |  | | | |  __/    | | (_) | (_) | |
    |_|  |_| |_|\___|    |_|\___/ \___/|_|
===========================================
");
#if DEBUG 
            Console.WriteLine("Running In Debug");
            var logFile = "log.txt";
            File.Delete(logFile);
#else
            Console.WriteLine("Running in not debug");
#endif
            Console.WriteLine("===========================================");


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
#if DEBUG
                .WriteTo.File(logFile)
#else
                //TODO Log to AWS sink for prod
#endif
                .CreateLogger();

            var pipeline = new PipelineBuilder()
                .With(new LogStage())
                .With(new WorkOrderResolutionStage())
                .With(new ResolveAddressStage())
                .With(new ResolveHierarchyDetails())
                .With(new FinanceIntegrationStage())
                .With(new ResolveCommunalStage())
                .Build();
#if DEBUG
            await CSVRunner.LoadSheetsForTest();
            await CSVRunner.ProcessCSVs(pipeline);
#else
            await GoogleRunner.ProcessGoogleSheets(pipeline);
#endif

            Log.CloseAndFlush();
        }
    }
}
