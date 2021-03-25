using Core;

namespace RepairsMigrator
{
    [MapPropName]
    public class IntermediateSheet
    {
        // Order
        public string Work_Order_Reference { get; set; }
        public string Trade { get; set; }
        public string Clients_Name { get; set; }
        public string Clients_Number { get; set; }
        public string Aggregated_Client_Info { get; set; }
        public string Agent_Name { get; set; }
        public string Created_Date { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Sor_Codes { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Current_Status { get; set; }
        public string Date_Raised { get; set; }
        public string Completion_Date { get; set; }
        public string Work_Order_completion_date { get; set; }
        public string Est_Cost_After_Variation { get; set; }
        public string Supplier_Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        // Property
        public string Original_Address { get; set; }
        public string Short_Address { get; set; }
        public string IsCommunal { get; set; }
        public string CommunalReason { get; set; }
        public string Property_Reference { get; set; }
        public string Sub_Block_Ref { get; set; }
        public string Sub_Block_Name { get; set; }
        public string Block_Ref { get; set; }
        public string Block_Name { get; set; }
        public string Estate_Ref { get; set; }
        public string Estate_Name { get; set; }
        public string Level_Description { get; set; }
        public string Neigh_Area { get; set; }

        // Financials
        public string HOC { get; set; }
        public string Nominal_Code { get; set; }
        public string Invoice_Number { get; set; }
        public string Date_Of_Invoice { get; set; }
        public string Actual_cost_of_invoice { get; set; }
        public string Cost_Code { get; set; }
        public string Corp_Subj_Code { get; set; }
        public string Local_Subj_Code { get; set; }
        public string Local_Subj { get; set; }
        public string Category_Type { get; set; }
        public string DLO_Split { get; set; }

        // Misc
        public string SourceRow { get; set; }
        public string SourceDescription { get; set; }

        public string NoFinance { get; set; }
        public string MultipleFinance { get; set; }
        public string MissingAddress { get; set; }
        public string MultipleAddresses { get; set; }
        public string CouldNotResolvePropRef { get; set; }
        public string NoPropRefForHierarchy { get; set; }
        public string FailedToMatchToASinglePropertyReference { get; set; }
    }
}
