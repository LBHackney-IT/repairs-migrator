namespace RepairsMigrator
{
    public static class Keys
    {
        // Target Data
        public const string Trade = "Trade";
        public const string Short_Address = "Short_Address";
        public const string Clients_Name = "Clients_Name";
        public const string Clients_Number = "Clients_Number";
        public const string Agent_Name = "Agent_Name";
        public const string Created_Date = "Created_Date";
        public const string Description = "Description";
        public const string Priority = "Priority";
        public const string Sor_Codes = "Sor_Codes";
        public const string AppointmentDate = "AppointmentDate";
        public const string AppointmentTime = "AppointmentTime";
        public const string Current_Status = "Current_Status";
        public const string Work_Order_Reference = "Work_Order_Reference";
        public const string Request_Reference = "Request_Reference";
        public const string Property_Reference = "Property_Reference";
        public const string Master_Block_Ref = "Master_Block_Ref";
        public const string Master_Block_Name = "Master_Block_Name";
        public const string Block_Range_Or_Estate = "Block_Range_Or_Estate";
        public const string HOC = "HOC";
        public const string Nominal_Code = "Nominal_Code";
        public const string Date_Raised = "Nominal_Code";
        public const string Completion_Date = "Completion_Date";
        public const string Work_Order_completion_date = "Work_Order_completion_date";
        public const string Est_Cost_After_Variation = "Est_Cost_After_Variation";
        public const string Invoice_Number = "Invoice_Number";
        public const string Date_Of_Invoice = "Date_Of_Invoice";
        public const string Actual_cost_of_invoice = "Actual_cost_of_invoice";
        public const string Level_Description = "Level_Description";
        public const string Cost_Code = "Cost_Code";
        public const string Corp_Subj_Code = "Corp_Subj_Code";
        public const string Local_Subj_Code = "Local_Subj_Code";
        public const string Local_Subj = "Local_Subj";
        public const string Category_Type = "Category_Type";
        public const string DLO_Split = "DLO_Split";
        public const string Supplier_Name = "Supplier_Name";
        public const string Neigh_Area = "Neigh_Area";
        public const string SourceRow = "SourceRow";
        public const string SourceDescription = "SourceDescription";

        // Intermediate  Keys (Stages need to map these to target keys)
        public const string Date = "Date";
        public const string Time = "Time"; // TODO: Build date and time into temp workorder
        public const string Aggregated_Client_Info = "Aggregated_Client_Info"; //  TODO: UnPick contact info
        public const string Sub_Block_Ref = "Sub_Block_Ref";
        public const string Sub_Block_Name = "Sub_Block_Name";
        public const string Block_Ref = "Block_Ref";
        public const string Block_Name = "Block_Name";
        public const string Estate_Ref = "Estate_Ref";
        public const string Estate_Name = "Estate_Name";
        public const string Level_Code = "Level_Code";
        public const string Level_Code_String = "Level_Code_String";
        public const string IsCommunal = "IsCommunal";
        public const string CommunalReason = "CommunalReason";
        public const string Original_Address = "Original_Address";
    }
}
