using System;
using Xunit;
using RepairsMigrator.Stages;
using System.Threading.Tasks;

namespace RepairsMigrator.Test
{
    public class FinanceIntegrationStageTests
    {
        [Fact]
        public async Task LoadFinanceSheet()
        {
            var sut = new FinanceIntegrationStage();

            await sut.Startup();
        }
    }
}
