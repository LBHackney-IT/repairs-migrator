using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveSupplierNameStage : PipelineStage<SupplierModel>
    {
        private readonly Dictionary<string, string> supplierNameMap = new Dictionary<string, string>
        {
            { "Door Entry", "" },
            { "Lightning Protection", "" },
            { "Lift Breakdown", "" },
            { "Fire Alarm/AOV", "" },
            { "Electrical Supplies", "" },
            { "Electric Heating", "" },
            { "Communal Lighting", "" },
            { "Emergency Lighting Servicing", "" },
            { "Reactive Rewires", "" },
            { "FRA Works", "" },
            { "Lift Servicing", "" },
            { "Heat Meters", "" },
            { "T.V Aerials", "" },
            { "DPA", "" },
            { "Mech. Repairs", "" },
            { "CP12", "" },
        };

        public override Task Process(SupplierModel model)
        {
            if (supplierNameMap.TryGetValue(model.SupplierName, out var newSupplierName))
            {
                model.SupplierName = newSupplierName;
            }

            return Task.CompletedTask;
        }
    }

    public class SupplierModel
    {
        [PropertyKey(Keys.Supplier_Name)]
        public string SupplierName { get; set; }
    }
}
