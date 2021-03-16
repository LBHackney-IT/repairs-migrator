using Core;

namespace RepairsMigrator.SheetModels
{
    class StannahSheet : IAppColumns
    {
        [PropertyKey(Keys.Created_Date)]
        public string Timestamp { get; set; }
        
        public string Email_address { get; set; }

        [PropertyKey(Keys.Short_Address)]
        public string Property_Address { get; set; }

        [PropertyKey(Keys.Work_Order_Reference)]
        public string Temporary_Order_Number { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }

        [PropertyKey(Keys.Cost_Code)]
        public string Budget_Code { get; set; }

        [PropertyKey(Keys.Priority)]
        public string Priority_Code { get; set; }

        [PropertyKey(Keys.Sor_Codes)]
        public string SORs { get; set; }
        
        public string VALUECosts { get; set; }
        
        public string NOTES_AND_INFORMATION { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
