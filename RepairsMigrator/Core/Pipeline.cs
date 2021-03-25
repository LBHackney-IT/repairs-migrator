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
        private readonly List<PropertyBag> bagsIn = new List<PropertyBag>();
        private IEnumerable<PropertyBag> bagsOut;

        internal Pipeline(IEnumerable<IBatchPipelineStage> stages) => this.stages = stages;

        public async Task Run()
        {
            bagsOut = bagsIn;

            foreach (var stage in stages)
            {
                bagsOut = await stage.Process(bagsOut);
            }
        }

        public IEnumerable<T> Out<T>()
            where T : class, new()
        {
            Log.Information("Outputting {count} records to type {typeName}", bagsOut.Count(), typeof(T).Name);
            return bagsOut.Select(b => b.To<T>()).ToList();
        }

        public void In<TIn>(IEnumerable<TIn> models)
            where TIn : class
        {
            Log.Information("Loading {count} records of type {typeName}", models.Count(), typeof(TIn).Name);
            bagsIn.AddRange(models.Select(m => PropertyBag.From(m)).ToList());
        }

        public void In(IEnumerable<object> models, Type inType)
        {
            Log.Information("Loading {count} records of type {typeName}", models.Count(), inType.Name);
            bagsIn.AddRange(models.Select(m => PropertyBag.From(m, inType)).ToList());
        }
    }
}
