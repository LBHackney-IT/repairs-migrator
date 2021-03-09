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

        public async Task<TOut> Run<TIn>(TIn testInModel)
            where TIn : class
        {
            var bag = PropertyBag.From(testInModel);

            foreach (var stage in stages)
            {
                await stage.Process(bag);
            }

            return bag.To<TOut>();
        }
    }
}
