using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator
{
    class LeaseHolderReportSheet
    {
        [Name("Work Order Reference")]
        public string WorkOrderReference { get; set; }

        [Name("Request Reference")]
        public string RequestReference { get; set; }

        [Name("Property Reference")]
        public string PropertyReference { get; set; }

        [Name("Master Block Ref")]
        public string MasterBlockRef { get; set; }

        [Name("Master Block")]
        public string MasterBlock { get; set; }

        [Name("Sub Block Ref")]
        public string SubBlockRef { get; set; }

        [Name("Sub Block")]
        public string SubBlock { get; set; }

        [Name("Block Range or Estate")]
        public string BlockRangeOrEstate { get; set; }

        [Name("Description")]
        public string Description { get; set; }

        [Name("Clean Description")]
        public string CleanDescription { get; set; }

        [Name("HOC")]
        public string HOC { get; set; }

        [Name("Nominal Code")]
        public string NominalCode { get; set; }

        [Name("Date Job Raised")]
        public string DateJobRaised { get; set; }

        [Name("Work Order Completion Date")]
        public string WorkOrderCompletionDate { get; set; }

        [Name("Date Completed")]
        public string DateCompleted { get; set; }

        [Name("Estimated Cost After Variation")]
        public string EstCostAfterVariation { get; set; }

        [Name("Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Name("Date of Invoice")]
        public string DateOfInvoice { get; set; }

        [Name("Actual Cost of Invoice")]
        public string ActualCostOfInvoice { get; set; }

        [Name("Level Description")]
        public string LevelDescription { get; set; }

        [Name("Corporate Subjective Code")]
        public string CorpSubjectiveCode { get; set; }

        [Name("Local Subjective Code")]
        public string LocalSubjectiveCode { get; set; }

        [Name("Local Subjective")]
        public string LocalSubjective { get; set; }

        [Name("Category Type")]
        public string CategoryType { get; set; }

        [Name("DLO Split")]
        public string DLOSplit { get; set; }

        [Name("Supplier Name")]
        public string SupplierName { get; set; }

        [Name("NeighArea")]
        public string NeighArea { get; set; }
    }
}
