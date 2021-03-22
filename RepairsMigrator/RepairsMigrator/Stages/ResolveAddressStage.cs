using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;
using Serilog;
using static DB.PropertyGateway;

namespace RepairsMigrator.Stages
{
    public class ResolveAddressStage : IBatchPipelineStage
    {
        public async Task Process(IEnumerable<PropertyBag> bags)
        {
             
             
            var gateway = new PropertyGateway();

            var addresses = bags
                .Where(b =>
                    !b.ContainsKey(Keys.Property_Reference)
                    || string.IsNullOrWhiteSpace(b[Keys.Property_Reference].ToString()))
                .Select(b => b[Keys.Short_Address].ToString())
                .Distinct();

            Log.Information("Resolving {count} Address to property references", addresses.Count());

            var map = await gateway.GetPropertyReferences(addresses);

            foreach (var bag in bags)
            {
                AttachPropRef(bag, map);
            }
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
