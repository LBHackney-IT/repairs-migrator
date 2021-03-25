using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator
{
    class LeaseHolderReportSheet
    {
        [PropertyKey(Keys.Work_Order_Reference)]
        [Name("Work Order Reference")]
        public string WorkOrderReference { get; set; }

        [PropertyKey(Keys.Request_Reference)]
        [Name("Request Reference")]
        public string RequestReference { get; set; }

        [PropertyKey(Keys.Property_Reference)]
        [Name("Property Reference")]
        public string PropertyReference { get; set; }

        [Name("Master Block Ref")]
        [PropertyKey(Keys.Grandparent_Reference)]
        public string MasterBlockRef { get; set; }

        [Name("Master Block")]
        [PropertyKey(Keys.Grandparent_Address)]
        public string MasterBlock { get; set; }

        [Name("Sub Block Ref")]
        [PropertyKey(Keys.Parent_Reference)]
        public string SubBlockRef { get; set; }

        [Name("Sub Block")]
        [PropertyKey(Keys.Parent_Address)]
        public string SubBlock { get; set; }

        [Name("Block Range or Estate")]
        [PropertyKey(Keys.Short_Address)]
        public string BlockRangeOrEstate { get; set; }

        [Name("Description")]
        [PropertyKey(Keys.Description)]
        public string Description { get; set; }

        [Name("Clean Description")]
        public string CleanDescription { get; set; }

        [Name("HOC")]
        [PropertyKey(Keys.HOC)]
        public string HOC { get; set; }

        [Name("Nominal Code")]
        [PropertyKey(Keys.Nominal_Code)]
        public string NominalCode { get; set; }

        [Name("Date Job Raised")]
        [PropertyKey(Keys.Created_Date)]
        public string DateJobRaised { get; set; }

        [Name("Work Order Completion Date")]
        [PropertyKey(Keys.Work_Order_completion_date)]
        public string WorkOrderCompletionDate { get; set; }

        [Name("Date Completed")]
        [PropertyKey(Keys.Completion_Date)]
        public string DateCompleted { get; set; }

        [Name("Estimated Cost After Variation")]
        [PropertyKey(Keys.Est_Cost_After_Variation)]
        public string EstCostAfterVariation { get; set; }

        [Name("Invoice Number")] //Finance
        [PropertyKey(Keys.Invoice_Number)]
        public string InvoiceNumber { get; set; }

        [Name("Date of Invoice")] //Finance
        [PropertyKey(Keys.Date_Of_Invoice)]
        public string DateOfInvoice { get; set; }

        [Name("Actual Cost of Invoice")] //Finance
        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string ActualCostOfInvoice { get; set; }

        [Name("Level Description")]
        [PropertyKey(Keys.Level_Description)]
        public string LevelDescription { get; set; }

        [Name("Corporate Subjective Code")] //Finance
        [PropertyKey(Keys.Corp_Subj_Code)]
        public string CorpSubjectiveCode { get; set; }

        [Name("Local Subjective Code")] //Finance
        [PropertyKey(Keys.Local_Subj_Code)]
        public string LocalSubjectiveCode { get; set; }

        [Name("Local Subjective")] //Finance
        [PropertyKey(Keys.Local_Subj)]
        public string LocalSubjective { get; set; }

        [Name("Category Type")]
        [PropertyKey(Keys.Category_Type)]
        public string CategoryType { get; set; }

        [Name("DLO Split")]
        [PropertyKey(Keys.DLO_Split)]
        public string DLOSplit { get; set; }

        [Name("Supplier Name")]
        [PropertyKey(Keys.Supplier_Name)]
        public string SupplierName { get; set; }

        [Name("NeighArea")]
        [PropertyKey(Keys.Neigh_Area)]
        public string NeighArea { get; set; }
    }
}
