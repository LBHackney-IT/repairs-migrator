using Core;

namespace RepairsMigrator.SheetModels
{
    /*
     * Each partial is taken from a different sheet in the file and then duplicates removed
     */

    partial class CMSheet : IAppColumns
    {
        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Address { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description { get; set; }

        [PropertyKey(Keys.Created_Date)]
        public string Date { get; set; }

        [PropertyKey(Keys.Work_Order_Reference)]
        public string Temp_Order_Number { get; set; }

        [PropertyKey(Keys.Priority)]
        public string Priority_Code { get; set; }

        public string Contractors_own_ref_no { get; set; }
        public string New_UHW_Number { get; set; }
        public string Requested_by { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }

    partial class CMSheet
    {
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Cost_of_RepairsWork { get; set; }
        
        [PropertyKey(Keys.Current_Status)]
        public string Status_of_Completed { get; set; }
    }

    partial class CMSheet
    {
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Raised_Value { get; set; }

        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string Total_invoiced { get; set; }
        
        [PropertyKey(Keys.Cost_Code)]
        public string Cost_code { get; set; }
        
        [PropertyKey(Keys.Current_Status)]
        public string Works_statuscomments { get; set; }
    }

    partial class CMSheet
    {
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Cost_of_Work { get; set; }
    }

    partial class CMSheet
    {
        [PropertyKey(Keys.Sor_Codes)]
        public string SOR { get; set; }
        
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Cost { get; set; }

        // TODO should only be 1 of these
        [PropertyKey(Keys.Corp_Subj_Code)]
        [PropertyKey(Keys.Local_Subj_Code)]
        public string Subjective { get; set; }
    }

    partial class CMSheet
    {
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string Contractor_Job_Status_Complete_or_in_Progress { get; set; }
        
        [PropertyKey(Keys.Completion_Date)]
        public string Date_Completed { get; set; }
    }

    partial class CMSheet
    {
        // TODO should only be 1 of these
        [PropertyKey(Keys.Corp_Subj_Code)]
        [PropertyKey(Keys.Local_Subj_Code)]
        public string Budget_Subjective { get; set; }
        
        public string TESS_Number { get; set; }
    }

    partial class CMSheet
    {
        public string Visits_Per_Year { get; set; }
    }
}
