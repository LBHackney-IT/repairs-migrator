using Core;

namespace RepairsMigrator.SheetModels
{
    class HertsSheet : IAppColumns
    {
        // Timestamp
        [PropertyKey(Keys.Created_Date)]
        public string zz { get; set; }
        
        public string Email_address { get; set; }
        
        [PropertyKey(Keys.Short_Address)]
        public string Property_Address { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }
        
        // Only one of these
        [PropertyKey(Keys.Corp_Subj_Code)]
        [PropertyKey(Keys.Local_Subj_Code)]
        public string Budget_Code { get; set; }
        
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Order_Value { get; set; }
        
        public string NOTES_AND_INFORMATION { get; set; }
        
        [PropertyKey(Keys.Priority)]
        public string Priority_Code { get; set; }

        [PropertyKey(Keys.Work_Order_Reference)]
        public string Temporary_Order_Number { get; set; }
        
        [PropertyKey(Keys.Aggregated_Client_Info)]
        public string CONTACT_INFORMATION_FOR_ACCESS { get; set; }
        
        [PropertyKey(Keys.Current_Status)]
        public string STATUS { get; set; }
        
        public string STATUS_NOTES { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
