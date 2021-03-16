using Core;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveHierarchyDetails : IBatchPipelineStage
    {
        public async Task Process(IEnumerable<PropertyBag> bags)
        {
            var gateway = new PropertyGateway();

            var references = bags
                .Select(b => b[Keys.Property_Reference].ToString())
                .Distinct();

            var details = await gateway.GetHierarchyDetails(references);

            foreach (var bag in bags)
            {
                AttachHierarchy(bag, details);
            }
        }

        private void AttachHierarchy(PropertyBag bag, IEnumerable<HierarchyModel> details)
        {
            throw new NotImplementedException();
        }
    }
}
