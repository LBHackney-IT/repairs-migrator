using Core;

namespace RepairsMigrator.SheetModels
{
    class AlphatrackSheet : IAppColumns
    {
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
