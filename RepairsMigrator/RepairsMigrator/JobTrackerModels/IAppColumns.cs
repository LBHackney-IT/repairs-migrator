using Core;
using Google;

namespace RepairsMigrator.SheetModels
{
    interface IAppColumns : IHasSourceColumns
    {
        [PropertyKey(Keys.Supplier_Name)]
        string Contractor { get; set; }
    }
}
