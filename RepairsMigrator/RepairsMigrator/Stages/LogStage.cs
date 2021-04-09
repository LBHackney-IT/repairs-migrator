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
        private readonly string message;

        public LogStage(string message)
        {
            this.message = message;
        }

        public Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            Log.Information(message, bags.Count());

            return Task.FromResult(bags);
        }
    }
}
