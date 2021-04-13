using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator
{
    class LeaseHolderReportSheet
    {
        [PropertyKey(Keys.Cost_Code)]
        [Name("Cost Code")]
        public string Cost_Code { get; set; }

        [PropertyKey(Keys.Corp_Subj_Code)]
        [Name("Corporate Subjective Code")]
        public string Corporate_Subjective_Code { get; set; }

        [Name("Description of Subjective code")]
        public string Description_of_Subjective_code { get; set; } = string.Empty;

        [PropertyKey(Keys.Property_Reference)]
        [Name("Property Reference")]
        public string Property_Reference { get; set; }

        [PropertyKey(Keys.Short_Address)]
        [Name("Address")]
        public string Address { get; set; }

        [PropertyKey(Keys.Parent_Address)]
        [Name("Block Range or Estate")]
        public string Block_Range_or_Estate { get; set; }

        [PropertyKey(Keys.Created_Date)]
        [Name("Date Raised")]
        public string Date_Raised { get; set; }

        [PropertyKey(Keys.Description)]
        [Name("Description of works")]
        public string Description_of_works { get; set; }

        [PropertyKey(Keys.Completion_Date)]
        [Name("Date Completed")]
        public string Date_Completed { get; set; }

        [PropertyKey(Keys.Actual_cost_of_invoice)]
        [Name("Actual cost after variation")]
        public string Actual_cost_after_variation { get; set; }

        [PropertyKey(Keys.Manager)]
        [Name("Manager responsible for work")]
        public string Manager_responsible_for_work { get; set; }

        [PropertyKey(Keys.Original_Address)]
        [Name("Original Address")]
        public string Original_Address { get; set; }

        [PropertyKey(Keys.Short_Address)]
        [Name("Matching Universal Housing Address")]
        public string UH_Address { get; set; }

        [PropertyKey(Keys.Paper_Order)]
        [Name("Original Completion Order")]
        public string Paper_Order { get; set; }
    }
}
