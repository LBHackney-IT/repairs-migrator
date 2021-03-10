using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class DLOSheet
    {
        public string Timestamp { get; set; }
        public string PlannerstoallocatetoOPERATIVES { get; set; }
        public string PlannersNotes { get; set; }
        public string Nameofresident { get; set; }
        public string Addressofrepair { get; set; }
        public string Phonenumberofresident { get; set; }
        public string Housingstatus_Istheresidenta { get; set; }
        [PropertyKey(Keys.Description)]
        public string Jobdescription { get; set; }
        public string Whichtradeneedstorespondtorepair { get; set; }
        public string Whatisthepriorityfortherepair { get; set; }
        public string Dateofappointment { get; set; }
        public string Timeofappointment { get; set; }
        public string RechargeorSusrecharge { get; set; }
        public string Isthereacautionarycontact { get; set; }
        public string Ifthereisacautionarycontactalert { get; set; }
        public string Doestheresidenthaveanyvulnerabilities { get; set; }
        public string Ifyeswhatvulnerabilitiesdotheyhave { get; set; }
        public string Emailaddress { get; set; }
        public string Postcodeofproperty { get; set; }
        public string UHPhoneNumber1 { get; set; }
        public string UHPhoneNumber2 { get; set; }
        public string BlockName { get; set; }
        public string EstateName { get; set; }
        public string Blockreference { get; set; }
        public string Estatereference { get; set; }
        public string UHPropertyReference { get; set; }

        [Constant("DLO")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}
