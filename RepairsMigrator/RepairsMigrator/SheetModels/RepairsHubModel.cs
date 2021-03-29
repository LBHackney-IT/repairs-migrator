using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.SheetModels
{
    public class RepairsHubModel
    {
        [PropertyKey(Keys.Work_Order_Reference)]
        public string Id { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description { get; set; }

        [PropertyKey(Keys.Created_Date)]
        public string DateRaised { get; set; }

        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Cost { get; set; }

        [PropertyKey(Keys.Property_Reference)]
        public string PropRef { get; set; }

        [PropertyKey(Keys.Completion_Date)]
        public string Completion_Time { get; set; }

        [PropertyKey(Keys.Trade)]
        public string TradeName { get; set; }

        [PropertyKey(Keys.Priority)]
        public string PriorityDescription { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}
