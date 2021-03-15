using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class AxisSheet : IAppColumns
    {
        [PropertyKey(Keys.Description)]
        public string Description_of_work { get; set; }

        [Constant("Axis")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
