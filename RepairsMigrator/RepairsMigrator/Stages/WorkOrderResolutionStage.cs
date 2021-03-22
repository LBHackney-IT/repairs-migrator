using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class WorkOrderResolutionStage : PipelineStage<WorkOrderModel>
    {
        public override Task Process(WorkOrderModel model)
        {
            if (string.IsNullOrWhiteSpace(model.WorkOrderReference))
            {
                model.WorkOrderReference = model.TimeStamp;
            }

            return Task.CompletedTask;
        }
    }

    internal class WorkOrderModel
    {
        [PropertyKey(Keys.Work_Order_Reference)]
        public string WorkOrderReference { get; set; }
        [PropertyKey(Keys.Created_Date)]
        public string TimeStamp { get; set; }
    }
}
