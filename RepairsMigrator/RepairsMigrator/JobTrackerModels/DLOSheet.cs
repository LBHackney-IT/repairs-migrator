using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class DLOSheet : IAppColumns
    {
        [PropertyKey(Keys.Created_Date)]
        public string Timestamp { get; set; }
        
        public string Planners_to_allocate_to_OPERATIVES { get; set; }
        
        public string Planners_Notes { get; set; }
        
        [PropertyKey(Keys.Clients_Name)]
        public string Name_of_resident { get; set; }
        
        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Address_of_repair { get; set; }
        
        [PropertyKey(Keys.Clients_Number)]
        public string Phone_number_of_resident { get; set; }
        
        // Tenant/TMO/FreeHold
        public string Housing_status_Is_the_resident_a { get; set; }
        
        [PropertyKey(Keys.Description)]
        public string Job_description { get; set; }
        
        [PropertyKey(Keys.Trade)]
        public string Which_trade_needs_to_respond_to_repair { get; set; }
        
        [PropertyKey(Keys.Priority)]
        public string What_is_the_priority_for_the_repair { get; set; }
        
        [PropertyKey(Keys.AppointmentDate)]
        public string Date_of_appointment { get; set; }
        
        [PropertyKey(Keys.AppointmentTime)]
        public string Time_of_appointment { get; set; }
        
        public string Is_the_job_a_Recharge_or_Sus_recharge { get; set; }
        
        public string Email_address { get; set; }
        
        public string Postcode_of_property { get; set; }
        
        public string UH_Phone_Number_1 { get; set; }
        
        public string UH_Phone_Number_2 { get; set; }
        
        public string Block_Name { get; set; }
        
        public string Estate_Name { get; set; }
        
        public string Block_reference { get; set; }
        
        public string Estate_reference { get; set; }
        
        [PropertyKey(Keys.Property_Reference)]
        public string UH_Property_Reference { get; set; }

        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}
