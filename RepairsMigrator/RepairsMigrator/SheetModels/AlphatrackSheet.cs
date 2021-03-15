﻿using Core;
using CsvHelper.Configuration.Attributes;

namespace RepairsMigrator.SheetModels
{
    class AlphatrackSheet : IAppColumns
    {
        [Constant("Alphatrack")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
        [PropertyKey(Keys.SourceDescription)]
        public string Source { get; set; }
        [PropertyKey(Keys.SourceRow)]
        public string Id { get; set; }
    }
}
