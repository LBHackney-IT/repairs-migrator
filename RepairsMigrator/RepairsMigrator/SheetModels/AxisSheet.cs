using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class AxisSheet
    {
        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }

        [Constant("Axis")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}
