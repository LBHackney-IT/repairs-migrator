using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class Pipeline<TOut>
        where TOut : class, new()
    {
        private readonly IEnumerable<IBatchPipelineStage> stages;

        internal Pipeline(IEnumerable<IBatchPipelineStage> stages) => this.stages = stages;

        public async Task<IEnumerable<TOut>> Run<TIn>(IEnumerable<TIn> models)
            where TIn : class
        {
            var bags = models.Select(m => PropertyBag.From(m)).ToList();

            foreach (var stage in stages)
            {
                await stage.Process(bags);
            }

            return bags.Select(b => b.To<TOut>()).ToList();
        }
    }
}
