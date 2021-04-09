using Core;

namespace RepairsMigrator.Stages
{
    public class FinanceData
    {
        [PropertyKey(Keys.SourceRow)]
        public string Line_No { get; set; }

        [MapPropName]
        public string Supplier_No { get; set; }
        
        [PropertyKey(Keys.Supplier_Name)]
        public string CedAr_Supplier { get; set; }

        [MapPropName]
        public string Cert_No { get; set; }
        
        [PropertyKey(Keys.Invoice_Number)]
        public string Invoice_No { get; set; }

        [PropertyKey(Keys.Date_Of_Invoice)]
        public string Invoice_date { get; set; }
        
        [PropertyKey(Keys.Work_Order_Reference)]
        public string UH_job_No { get; set; }

        [PropertyKey(Keys.Property_Reference)]
        public string Property_ref { get; set; }

        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Address { get; set; }

        [MapPropName]
        public string Area_Office { get; set; }

        [MapPropName]
        public string Cost_Centre { get; set; }

        [PropertyKey(Keys.Corp_Subj_Code)]
        public string Corp_subj { get; set; }

        [PropertyKey(Keys.Local_Subj_Code)]
        public string Local_subj { get; set; }

        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string Amount { get; set; }

        [MapPropName]
        public string Month { get; set; }

        [MapPropName]
        public string SCA { get; set; }

        [MapPropName]
        public string Contract { get; set; }

        [MapPropName]
        public string Name_of_CPA { get; set; }

        [PropertyKey(Keys.Description)]
        public string Job_Description { get; set; }
    }
}
