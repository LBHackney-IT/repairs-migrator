﻿using Core;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.SheetModels
{
    class AlphatrackSheet
    {
        [Constant("Alphatrack")]
        [PropertyKey(Keys.Supplier_Name)]
        public string Contractor { get; set; }
    }
}