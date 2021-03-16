using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class PropertyGateway
    {
        public Task<Dictionary<string, string>> GetPropertyReferences(IEnumerable<string> addresses)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HierarchyModel>> GetHierarchyDetails(IEnumerable<string> references)
        {
            throw new NotImplementedException();
        }
    }

    public class HierarchyModel
    {
        public string PropReference { get; set; }
        public string PropAddress { get; set; }
        public string SubblockReference { get; set; }
        public string SubblockAddress { get; set; }
        public string BlockReferece { get; set; }
        public string BlockAdress { get; set; }
        public string EstateReference { get; set; }
        public string EstateAddress { get; set; }
    }
}
