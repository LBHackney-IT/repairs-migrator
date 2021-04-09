using Core;

namespace RepairsMigrator
{
    public class CommunitiveSupplierSheet
    {
        [PropertyKey(Keys.SourceRow)]
        public string Line_No { get; set; }

        [MapPropName] public string Supplier_No { get; set; }

        [PropertyKey(Keys.Supplier_Name)]
        public string CedAr_Supplier { get; set; }

        [MapPropName] public string Cert_No { get; set; }

        [PropertyKey(Keys.Invoice_Number)]
        public string Invoice_No { get; set; }

        [PropertyKey(Keys.Date_Of_Invoice)]
        public string Invoice_date { get; set; }

        [PropertyKey(Keys.Work_Order_Reference)]
        public string UH_job_No { get; set; }

        [PropertyKey(Keys.Property_Reference)]
        public string Property_ref { get; set; }

        [PropertyKey(Keys.Short_Address)]
        public string Address { get; set; }

        [MapPropName] public string Area_Office { get; set; }

        [MapPropName] public string Cost_Centre { get; set; }

        [PropertyKey(Keys.Corp_Subj_Code)]
        public string Corp_subj { get; set; }

        [PropertyKey(Keys.Local_Subj_Code)]
        public string Local_subj { get; set; }

        [PropertyKey(Keys.Actual_cost_of_invoice)]
        public string Amount { get; set; }

        [MapPropName] public string Month { get; set; }

        [MapPropName] public string SCA { get; set; }

        [MapPropName] public string Contract { get; set; }

        [MapPropName] public string Name_of_CPA { get; set; }

        [PropertyKey(Keys.Description)]
        public string Job_Description { get; set; }

        [PropertyKey(Keys.Parent_Reference)]
        public string ParentReference { get; set; }

        [PropertyKey(Keys.Parent_Address)]
        public string ParentAddress { get; set; }

        [PropertyKey(Keys.Grandparent_Reference)]
        public string GrandParentReference { get; set; }

        [PropertyKey(Keys.Grandparent_Address)]
        public string GrandParentAddress { get; set; }

        [MapPropName] public string Sub_Block_Name { get; set; }
        [MapPropName] public string Sub_Block_Ref { get; set; }
        [MapPropName] public string Block_Ref { get; set; }
        [MapPropName] public string Block_Name { get; set; }
        [MapPropName] public string Estate_Ref { get; set; }
        [MapPropName] public string Estate_Name { get; set; }
        [MapPropName] public string Level_Code { get; set; }
        [MapPropName] public string Owner_ref { get; set; }
        [MapPropName] public string Owner_Name { get; set; }
        [MapPropName] public string Level_Description { get; set; }
        [MapPropName] public string Neigh_Area { get; set; }
        [MapPropName] public string Category_Type { get; set; }

        [PropertyKey(Keys.IsCommunal)]
        public string IsCommunal { get; set; }

        [PropertyKey(Keys.CommunalReason)]
        public string CommunalReason { get; set; }

        [PropertyKey(Keys.Original_Address)]
        public string OriginalAddress { get; set; }
    }
}
