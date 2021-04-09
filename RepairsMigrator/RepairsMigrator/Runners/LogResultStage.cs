using Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairsMigrator.Runners
{
    internal class LogResultStage : IBatchPipelineStage
    {
        public Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            var list = bags.ToList();
            int overallCount = list.Count;
            int countWithCost = list.Count(b => !string.IsNullOrWhiteSpace(b.GetMaybe(Keys.Actual_cost_of_invoice)));
            double costPercentage = ((double)countWithCost / overallCount) * 100;

            Log.Information("Result Count {count}", overallCount);
            Log.Information("Result With Cost {count} ({percent}%)", countWithCost, Math.Round(costPercentage, 2));

            return Task.FromResult(bags);
        }
    }
}