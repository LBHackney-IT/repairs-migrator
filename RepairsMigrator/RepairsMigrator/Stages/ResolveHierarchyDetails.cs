using Core;
using DB;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveHierarchyDetails : IBatchPipelineStage
    {
        public async Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            var gateway = new PropertyGateway();

            var references = bags
                .Where(b => b.ContainsKey(Keys.Property_Reference) && !string.IsNullOrWhiteSpace(b[Keys.Property_Reference]?.ToString()))
                .Select(b => b[Keys.Property_Reference].ToString())
                .Distinct();

            Log.Information("Resolving {count} Unique Property Hierarchies", references.Count());

            var details = await PropertyGateway.GetHierarchyDetails(references);

            foreach (var bag in bags)
            {
                AttachHierarchy(bag, details.ToDictionary(d => d.PropReference.Trim()));
            }

            return bags;
        }

        private static void AttachHierarchy(PropertyBag bag, Dictionary<string, HierarchyModel> details)
        {
            if (!bag.ContainsKey(Keys.Property_Reference) || string.IsNullOrWhiteSpace(bag[Keys.Property_Reference]?.ToString()))
            {
                bag.AddError(ErrorKeys.NoPropRefForHierarchy);
                return;
            }
            var propRef = bag[Keys.Property_Reference]?.ToString();

            if (details.TryGetValue(propRef.Trim(), out var data))
            {
                bag[Keys.Sub_Block_Name] = data.SubblockAddress;
                bag[Keys.Sub_Block_Ref] = data.SubblockReference;
                bag[Keys.Block_Ref] = data.BlockReference;
                bag[Keys.Block_Name] = data.BlockAddress;
                bag[Keys.Estate_Ref] = data.EstateReference;
                bag[Keys.Estate_Name] = data.EstateAddress;
                bag[Keys.Level_Code] = data.LevelCode;
                bag[Keys.Owner_ref] = data.OwnerReference;
                bag[Keys.Owner_Name] = data.OwnerAddress;
                bag[Keys.Level_Description] = data.Level_Description;
                bag[Keys.Neigh_Area] = data.Rep_Area;
                bag[Keys.Category_Type] = data.Category_Type;
            } else
            {
                bag.AddError(ErrorKeys.FailedToMatchToASinglePropertyReference);
            }


        }
    }
}
