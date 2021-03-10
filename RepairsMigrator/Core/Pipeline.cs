using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public class Pipeline<TOut>
        where TOut : class, new()
    {
        private readonly IEnumerable<IPipelineStage> stages;

        internal Pipeline(IEnumerable<IPipelineStage> stages) => this.stages = stages;

        public async Task<TOut> Run<TIn>(TIn model)
            where TIn : class
        {
            var bag = PropertyBag.From(model);

            foreach (var stage in stages)
            {
                await stage.Process(bag);
            }

            return bag.To<TOut>();
        }

        public async Task<IEnumerable<TOut>> RunBatch<TIn>(IEnumerable<TIn> models)
            where TIn : class
        {
            List<TOut> outs = new List<TOut>();

            foreach (var model in models)
            {
                outs.Add(await Run(model));
            }

            return outs;
        }
    }
}
