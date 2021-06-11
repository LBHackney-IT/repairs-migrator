using Core;

namespace RepairsMigrator.JobTrackerModels
{
    public class PestControl
    {
        [MapPropName]
        public string Marker { get; set; } = "PEST";
        [MapPropName]
        public string Rev_Order { get; set; }
        [MapPropName]
        public string Order_Number { get; set; }
        [MapPropName]
        public string Date_Created { get; set; }
        [MapPropName]
        public string SOR { get; set; }
        [MapPropName]
        public string Requested_By { get; set; }
        [MapPropName]
        public string Supplier { get; set; }
        [MapPropName]
        public string Trade { get; set; }
        [MapPropName]
        public string Completed { get; set; }
        [MapPropName]
        public string Budget { get; set; }
        [MapPropName]
        public string Budget_Holder { get; set; }
        [MapPropName]
        public string Cost_Code { get; set; }
        [MapPropName]
        public string Coporate_Subjective { get; set; }
        [MapPropName]
        public string Address { get; set; }
        [MapPropName]
        public string Description { get; set; }
        [MapPropName]
        public string Job_Description { get; set; }
        [MapPropName]
        public string Unit { get; set; }
        [MapPropName]
        public string Unit_Cost { get; set; }
        [MapPropName]
        public string Revised_Cost { get; set; }
        [MapPropName]
        public string Invoice_Ref { get; set; }
        [MapPropName]
        public string Paid_Date { get; set; }
        [MapPropName]
        public string Match_with_LIS_report { get; set; }
        [MapPropName]
        public string Match_CSL { get; set; }
        [MapPropName]
        public string Matched_Items { get; set; }
        [MapPropName]
        public string Relevant_or_Removed { get; set; }
        [MapPropName]
        public string Reason { get; set; }
        [MapPropName]
        public string Estate_Reference { get; set; }
        [MapPropName]
        public string Block_Reference { get; set; }
    }
}
