using Core;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.SheetModels
{
    class AvonlineSheet : IAppColumns
    {
        [PropertyKey(Keys.Created_Date)]
        public string Timestamp { get; set; }
        
        public string Email_address { get; set; }
        
        [PropertyKey(Keys.Date)]
        public string Date { get; set; }
        
        [PropertyKey(Keys.Time)]
        public string Time { get; set; }
        
        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Property_Address { get; set; }
        
        [PropertyKey(Keys.Aggregated_Client_Info)]
        public string CONTACT_INFORMATION { get; set; }
        
        [PropertyKey(Keys.Work_Order_Reference)]
        public string Temporary_Order_Number { get; set; }

        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }
        
        [PropertyKey(Keys.Sor_Codes)]
        public string CALL_OUT_SORs { get; set; }

        [PropertyKey(Keys.Cost_Code)]
        public string Budget_Code { get; set; }
        
        [PropertyKey(Keys.Priority)]
        public string Priority_Code { get; set; }
        
        public string NOTES_AND_INFORMATION { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
