using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.JobTrackerModels
{
    public class DLO
    {
        [MapPropName]
        public string Marker { get; set; } = "DLO";
        [MapPropName]
        public string Cost_Code { get; set; }
        [MapPropName]
        public string Corporate_Subjective_Code { get; set; }
        [MapPropName]
        public string Description_of_Subjective_code { get; set; }
        [MapPropName]
        public string Job_Number { get; set; }
        [MapPropName]
        public string Revised_Property_Refrences { get; set; }
        [MapPropName]
        public string Property_Reference { get; set; }
        [MapPropName]
        public string Address { get; set; }
        [MapPropName]
        public string Block_Range_or_Estate { get; set; }
        [MapPropName]
        public string Description_of_works { get; set; }
        [MapPropName]
        public string Date_Completed { get; set; }
        [MapPropName]
        public string Total_cost_after_variation { get; set; }
        [MapPropName]
        public string Estate_Reference { get; set; }
        [MapPropName]
        public string Block_Reference { get; set; }
    }
}
