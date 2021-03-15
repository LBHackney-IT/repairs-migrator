using Core;
using RepairsMigrator.Runners;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            var logFile = "log.txt";
            File.Delete(logFile);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFile)
                .CreateLogger();

            var pipeline = new PipelineBuilder().Build<TargetOutputSheet>();

            await GoogleRunner.ProcessGoogleSheets(pipeline);

            //await CSVRunner.ProcessCSVs(pipeline);
            Log.CloseAndFlush();
        }
    }
}
