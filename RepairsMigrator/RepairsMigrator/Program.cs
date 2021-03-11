using Core;
using RepairsMigrator.Runners;
using System.Threading.Tasks;

namespace RepairsMigrator
{
    class Program
    {
        static async Task Main()
        {
            var pipeline = new PipelineBuilder().Build<TargetOutputSheet>();

            await GoogleRunner.ProcessGoogleSheets(pipeline);

            await CSVRunner.ProcessCSVs(pipeline);
        }
    }
}
