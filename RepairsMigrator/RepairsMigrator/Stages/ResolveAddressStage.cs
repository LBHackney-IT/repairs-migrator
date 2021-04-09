using Core;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DB.PropertyGateway;

namespace RepairsMigrator.Stages
{
    public class ResolveAddressStage : IBatchPipelineStage
    {
        private int successCount;

        public async Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            var map = await GetPropertyReferences();

            this.successCount = 0;

            foreach (var bag in bags)
            {
                AttachPropRef(bag, map);
            }

            Log.Information("Resolved {count} addresses to property reference", successCount);

            return bags;
        }

        private void AttachPropRef(PropertyBag bag, Dictionary<string, PropRefModel> map)
        {
            var existingPropRef = bag.GetMaybe(Keys.Property_Reference);
            var address = bag.GetMaybe(Keys.Short_Address);

            if (existingPropRef.IsNotNull() && int.TryParse(existingPropRef, out _)) return;
            if (address.IsNull())
            {
                bag.AddError(ErrorKeys.MissingAddress);
                return;
            }

            if (map.TryGetValue(address, out var newPropRef))
            {
                bag[Keys.Property_Reference] = newPropRef.PropRef;
                bag[Keys.Short_Address] = newPropRef.ResolvedAddress;
                successCount++;
                return;
            }

            bag.AddError(ErrorKeys.FailedToMatchToASinglePropertyReference);
        }
    }
}
