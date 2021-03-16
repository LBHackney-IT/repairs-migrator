using Core;

namespace RepairsMigrator.SheetModels
{
    class StannahSheet : IAppColumns
    {
        public string Timestamp { get; set; }
        
        public string Email_address { get; set; }
        
        public string Property_Address { get; set; }
        
        public string Temporary_Order_Number { get; set; }
        
        public string Description_of_work { get; set; }
        
        public string Budget_Code { get; set; }
        
        public string Priority_Code { get; set; }
        
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
