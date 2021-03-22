using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class PurdySheet : IAppColumns
    {
        [PropertyKey(Keys.Created_Date)]
        public string Timestamp { get; set; }
        
        public string Email_address { get; set; }

        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Property_Address { get; set; }

        [PropertyKey(Keys.Work_Order_Reference)]
        public string Temporary_Order_Number { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }

        [PropertyKey(Keys.Cost_Code)]
        public string Budget_Code { get; set; }

        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Order_Value { get; set; }
        
        public string NOTES_AND_INFORMATION { get; set; }

        [PropertyKey(Keys.Priority)]
        public string Priority { get; set; }

        [PropertyKey(Keys.Date)]
        public string Date { get; set; }

        [PropertyKey(Keys.Time)]
        public string Time { get; set; }

        [PropertyKey(Keys.Completion_Date)]
        [PropertyKey(Keys.Work_Order_completion_date)]
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
