using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class StannahSheet
    {
        [Constant("Stannah")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}
