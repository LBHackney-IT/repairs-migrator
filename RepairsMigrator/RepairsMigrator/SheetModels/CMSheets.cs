using Core;

namespace RepairsMigrator.SheetModels
{
    /*
     * Each partial is taken from a different sheet in the file and then duplicates removed
     */

    partial class CMSheet : IAppColumns
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Temp_Order_Number { get; set; }
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
        public string Cost_of_RepairsWork { get; set; }
        public string Status_of_Completed { get; set; }
    }

    partial class CMSheet
    {
        public string Raised_Value { get; set; }
        public string Total_invoiced { get; set; }
        public string Cost_code { get; set; }
        public string Works_statuscomments { get; set; }
    }

    partial class CMSheet
    {
        public string Cost_of_Work { get; set; }
    }

    partial class CMSheet
    {
        public string SOR { get; set; }
        public string Cost { get; set; }
        public string Subjective { get; set; }
    }

    partial class CMSheet
    {
        public string Contractor_Job_Status_Complete_or_in_Progress { get; set; }
        public string Date_Completed { get; set; }
    }

    partial class CMSheet
    {
        public string Budget_Subjective { get; set; }
        public string TESS_Number { get; set; }
    }

    partial class CMSheet
    {
        public string Visits_Per_Year { get; set; }
    }
}
