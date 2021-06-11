using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.JobTrackerModels
{
    public class Communal
    {
        [MapPropName]
        public string Marker { get; set; } = "COMMUNAL";
        [MapPropName]
        public string CedAr_Supplier { get; set; }
        [MapPropName]
        public string Cert_No { get; set; }
        [MapPropName]
        public string Invoice_No_Revised { get; set; }
        [MapPropName]
        public string Invoice_No { get; set; }
        [MapPropName]
        public string Invoice_date { get; set; }
        [MapPropName]
        public string Revised_UH_Number { get; set; }
        [MapPropName]
        public string UH_job_No { get; set; }
        [MapPropName]
        public string Revised_Property_Ref { get; set; }
        [MapPropName]
        public string Property_ref { get; set; }
        [MapPropName]
        public string Address { get; set; }
        [MapPropName]
        public string Area_Office { get; set; }
        [MapPropName]
        public string Cost_Centre { get; set; }
        [MapPropName]
        public string cost_code_description { get; set; }
        [MapPropName]
        public string Corp_subj_Revised { get; set; }
        [MapPropName]
        public string Corp_sub_description { get; set; }
        [MapPropName]
        public string Corp_subj { get; set; }
        [MapPropName]
        public string Local_subj { get; set; }
        [MapPropName]
        public string Amount { get; set; }
        [MapPropName]
        public string Job_Description { get; set; }
        [MapPropName]
        public string Date_Paid_final { get; set; }
        [MapPropName]
        public string Relevant_or_Not_Relevant { get; set; }
        [MapPropName]
        public string Capital_or_Revenue { get; set; }
        [MapPropName]
        public string Analysis_CSL { get; set; }
        [MapPropName]
        public string Estate_Reference { get; set; }
        [MapPropName]
        public string Block_Reference { get; set; }
    }

}
