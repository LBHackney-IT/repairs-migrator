using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IPipelineStage
    {
        Task Process(PropertyBag bag);
    }

    public abstract class PipelineStage<T> : IPipelineStage
        where T : class, new()
    {
        private PropertyBag bag;

        public async Task Process(PropertyBag bag)
        {
            this.bag = bag;
            T model = bag.To<T>();
            await Process(model);
            bag.Apply(model);
        }

        protected void LogError(string error)
        {
            bag.AddError(error);
        }

        public abstract Task Process(T model);
    }
}
