using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.JobTrackerModels
{
    public class CCTV
    {
        [MapPropName]
        public string Marker { get; set; } = "CCTV";
        [MapPropName]
        public string Revised_Order { get; set; }
        [MapPropName]
        public string Order_Number { get; set; }
        [MapPropName]
        public string Date_Received { get; set; }
        [MapPropName]
        public string Property { get; set; }
        [MapPropName]
        public string Description { get; set; }
        [MapPropName]
        public string Date_Completed { get; set; }
        [MapPropName]
        public string Job_Sheet { get; set; }
        [MapPropName]
        public string Revised_Cost { get; set; }
        [MapPropName]
        public string Cost { get; set; }
        [MapPropName]
        public string Cost_Code { get; set; }
        [MapPropName]
        public string Comment { get; set; }
        [MapPropName]
        public string Month { get; set; }
        [MapPropName]
        public string Cert_No { get; set; }
        [MapPropName]
        public string Revised_Invoice_ref { get; set; }
        [MapPropName]
        public string Invoie_ref { get; set; }
        [MapPropName]
        public string Paid_Date { get; set; }
        [MapPropName]
        public string Matched_with_LIS_Report { get; set; }
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
