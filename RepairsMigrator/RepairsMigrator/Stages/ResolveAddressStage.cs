using Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DB.PropertyGateway;

namespace RepairsMigrator.Stages
{
    public class ResolveAddressStage : IBatchPipelineStage
    {
        public async Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            var map = await GetPropertyReferences();

            foreach (var bag in bags)
            {
                AttachPropRef(bag, map);
            }

            return bags;
        }

        private static void AttachPropRef(PropertyBag bag, Dictionary<string, PropRefModel> map)
        {
            var existingPropRef = bag.GetMaybe(Keys.Property_Reference);
            var address = bag.GetMaybe(Keys.Short_Address);

            if (existingPropRef.IsNotNull()) return;
            if (address.IsNull())
            {
                bag.AddError(ErrorKeys.MissingAddress);
                return;
            }

            if (map.TryGetValue(address, out var newPropRef))
            {
                bag[Keys.Property_Reference] = newPropRef.PropRef;
                bag[Keys.Short_Address] = newPropRef.ResolvedAddress;
                return;
            }

            bag.AddError(ErrorKeys.FailedToMatchToASinglePropertyReference);
        }
    }
}
