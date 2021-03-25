using Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class LogStage : IBatchPipelineStage
    {
        public Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            Log.Information("Processing {count} Records", bags.Count());

            return Task.FromResult(bags);
        }
    }
}
