using Core;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class LoadAddressStoreStage : IBatchPipelineStage
    {
        public async Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            var addresses = bags
                .Where(b =>
                    !b.ContainsKey(Keys.Property_Reference)
                    || string.IsNullOrWhiteSpace(b[Keys.Property_Reference].ToString()))
                .Select(b => b[Keys.Short_Address].ToString())
                .Distinct();

            await PropertyGateway.LoadAddressStore(addresses);

            return bags;
        }
    }
}
