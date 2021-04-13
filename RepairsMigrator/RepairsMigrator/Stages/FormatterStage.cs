using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    public class FormatterStage : IBatchPipelineStage
    {
        private readonly string key;
        private readonly Func<string, string> transform;

        public FormatterStage(string key, Func<string, string> transform)
        {
            this.key = key;
            this.transform = transform;
        }

        public Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            foreach (var bag in bags)
            {
                var value = bag.GetMaybe(this.key);

                var newValue = transform(value);

                bag[key] = newValue;
            }

            return Task.FromResult(bags);
        }
    }
}
