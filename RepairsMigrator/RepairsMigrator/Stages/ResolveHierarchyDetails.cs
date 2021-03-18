﻿using Core;
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
        public async Task Process(IEnumerable<PropertyBag> bags)
        {
            var gateway = new PropertyGateway();

            var references = bags
                .Where(b => b.ContainsKey(Keys.Property_Reference) && !string.IsNullOrWhiteSpace(b[Keys.Property_Reference].ToString()))
                .Select(b => b[Keys.Property_Reference].ToString())
                .Distinct();

            Log.Information("Resolving {count} Unique Property Hierarchies", references.Count());

            var details = await gateway.GetHierarchyDetails(references);

            foreach (var bag in bags)
            {
                AttachHierarchy(bag, details.ToDictionary(d => d.PropReference.Trim()));
            }
        }

        private void AttachHierarchy(PropertyBag bag, Dictionary<string, HierarchyModel> details)
        {
            if (!bag.ContainsKey(Keys.Property_Reference) || string.IsNullOrWhiteSpace(bag[Keys.Property_Reference].ToString()))
            {
                bag.AddError("No property reference to map address to");
                return;
            }
            var propRef = bag[Keys.Property_Reference].ToString();

            if (details.TryGetValue(propRef.Trim(), out var data))
            {
                bag[Keys.Sub_Block_Name] = data.SubblockAddress;
                bag[Keys.Sub_Block_Ref] = data.SubblockReference;
                bag[Keys.Block_Ref] = data.BlockReference;
                bag[Keys.Block_Name] = data.BlockAddress;
                bag[Keys.Estate_Ref] = data.EstateReference;
                bag[Keys.Estate_Name] = data.EstateAddress;
            } else
            {
                bag.AddError("No Matching property was found for property reference");
            }


        }
    }
}