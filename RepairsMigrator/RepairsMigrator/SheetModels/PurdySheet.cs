using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class PurdySheet : IAppColumns
    {
        public string Timestamp { get; set; }
        
        public string Email_address { get; set; }
        
        public string Property_Address { get; set; }
        
        public string Temporary_Order_Number { get; set; }
        
        public string Description_of_work { get; set; }
        
        public string Budget_Code { get; set; }
        
        public string Order_Value { get; set; }
        
        public string NOTES_AND_INFORMATION { get; set; }
        
        public string Priority { get; set; }
        
        public string Date { get; set; }
        
        public string Time { get; set; }
        
        public string Date_Completed { get; set; }
        
        public string Additional_Notes { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
