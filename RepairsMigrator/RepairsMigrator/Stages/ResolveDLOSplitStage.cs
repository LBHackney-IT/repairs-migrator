using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveDLOSplitStage : IPipelineStage
    {
        public Task Process(PropertyBag bag)
        {
            bag[Keys.DLO_Split] = bag.TryGetValue(Keys.Supplier_Name, out var sup) && sup?.ToString() == "DLO" ? "DLO" : "Non-DLO";
            return Task.CompletedTask;
        }

        public Task Startup() => Task.CompletedTask;

        public Task TearDown() => Task.CompletedTask;
    }
}
