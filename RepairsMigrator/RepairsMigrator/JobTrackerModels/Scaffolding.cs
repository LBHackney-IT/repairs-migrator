using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.JobTrackerModels
{
    public class Scaffolding
    {
        [MapPropName]
        public string Marker { get; set; } = "SCAF";
        [MapPropName]
        public string Revised_Order { get; set; }
        [MapPropName]
        public string Job_order { get; set; }
        [MapPropName]
        public string val { get; set; }
        [MapPropName]
        public string check { get; set; }
        [MapPropName]
        public string audit { get; set; }
        [MapPropName]
        public string Date_created { get; set; }
        [MapPropName]
        public string Raised_by { get; set; }
        [MapPropName]
        public string Supplier { get; set; }
        [MapPropName]
        public string trade { get; set; }
        [MapPropName]
        public string Date_completed { get; set; }
        [MapPropName]
        public string Completed_by { get; set; }
        [MapPropName]
        public string Date_modified { get; set; }
        [MapPropName]
        public string Area_Office { get; set; }
        [MapPropName]
        public string Local_subj { get; set; }
        [MapPropName]
        public string Corp_subj { get; set; }
        [MapPropName]
        public string Job_status { get; set; }
        [MapPropName]
        public string Full_address { get; set; }
        [MapPropName]
        public string Prop_ref { get; set; }
        [MapPropName]
        public string Prop_type { get; set; }
        [MapPropName]
        public string Sub_type { get; set; }
        [MapPropName]
        public string Prop_cat_type { get; set; }
        [MapPropName]
        public string Job_description { get; set; }
        [MapPropName]
        public string Original_cost { get; set; }
        [MapPropName]
        public string Revised_cost { get; set; }
        [MapPropName]
        public string variation { get; set; }
        [MapPropName]
        public string Variation_By { get; set; }
        [MapPropName]
        public string Variation_Auth_By { get; set; }
        [MapPropName]
        public string budget { get; set; }
        [MapPropName]
        public string Comment { get; set; }
        [MapPropName]
        public string Invoice_ref { get; set; }
        [MapPropName]
        public string Paid_Date { get; set; }
        [MapPropName]
        public string Match_LIS_REPORT { get; set; }
        [MapPropName]
        public string MATCH_CSL { get; set; }
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
