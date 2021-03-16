using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class Pipeline
    {
        private readonly IEnumerable<IBatchPipelineStage> stages;
        private List<PropertyBag> bags = new List<PropertyBag>();

        internal Pipeline(IEnumerable<IBatchPipelineStage> stages) => this.stages = stages;

        public async Task Run()
        {
            foreach (var stage in stages)
            {
                await stage.Process(bags);
            }
        }

        public IEnumerable<T> Out<T>()
            where T : class, new()
        {
            Log.Information("Outputting {count} records to type {typeName}", bags.Count(), typeof(T).Name);
            return bags.Select(b => b.To<T>()).ToList();
        }

        public void In<TIn>(IEnumerable<TIn> models)
            where TIn : class
        {
            Log.Information("Loading {count} records of type {typeName}", models.Count(), typeof(TIn).Name);
            bags.AddRange(models.Select(m => PropertyBag.From(m)).ToList());
        }

        public void In(IEnumerable<object> models, Type inType)
        {
            Log.Information("Loading {count} records of type {typeName}", models.Count(), inType.Name);
            bags.AddRange(models.Select(m => PropertyBag.From(m, inType)).ToList());
        }
    }
}
