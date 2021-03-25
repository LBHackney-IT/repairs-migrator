using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Filters
{
    class CommunalFilter : IFilter
    {
        public bool IsValid(PropertyBag bag)
        {
            return bag.TryGetValue(Keys.IsCommunal, out var ic) && ic?.ToString() == "True";
        }
    }
}
