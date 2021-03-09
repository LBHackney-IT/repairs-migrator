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
        public async Task Process(PropertyBag bag)
        {
            T model = bag.To<T>();
            await Process(model);
            bag.Apply(model);
        }

        public abstract Task Process(T model);
    }
}
