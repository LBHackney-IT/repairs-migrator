using Core;

namespace RepairsMigrator
{
    public class ProFormaSheet
    {
        [PropertyKey(Keys.Created_Date)]
        public string Reference_number_of_proforma { get; set; }

        [PropertyKey(Keys.Trade)]
        public string Trade { get; set; }

        [PropertyKey(Keys.Priority)]
        public string Priority { get; set; }
        public string Logging_number { get; set; }

        [PropertyKey(Keys.Completion_Date)]
        public string Date_of_response { get; set; }

        [PropertyKey(Keys.Short_Address)]
        [PropertyKey(Keys.Original_Address)]
        public string Address { get; set; }

        public string Op { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Action_by_Op { get; set; }
        public string Comments_from_op { get; set; }

        [PropertyKey(Keys.Paper_Order)]
        public string Scanned_copy_location_on_Google_Drive { get; set; }
        public string Material_used { get; set; }
        public string Requisition_number { get; set; }
        public string Recharge_required { get; set; }
        public string Comments { get; set; }
        public string Schedule { get; set; }
        public string time_value { get; set; }
        public string tirm_value_hrs { get; set; }

        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string Pounds { get; set; }
        public string Uni_value { get; set; }
        public string scheme { get; set; }
        public string Bonus_process_date { get; set; }
        public string Period_number { get; set; }
        public string Financial_week_number { get; set; }
        public string Financial_year { get; set; }
    }
}
