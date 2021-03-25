using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public interface IBatchPipelineStage
    {
        Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags);
    }

    class BatchWrapper : IBatchPipelineStage
    {
        private readonly IPipelineStage stage;

        public BatchWrapper(IPipelineStage stage)
        {
            this.stage = stage;
        }

        public async Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            await stage.Startup();

            foreach (var item in bags)
            {
                await stage.Process(item);
            }

            await stage.TearDown();

            return bags;
        }
    }

    class FilterWrapper : IBatchPipelineStage
    {
        private readonly IFilter filter;

        public FilterWrapper(IFilter filter)
        {
            this.filter = filter;
        }

        public Task<IEnumerable<PropertyBag>> Process(IEnumerable<PropertyBag> bags)
        {
            return Task.FromResult(bags.Where(filter.IsValid));
        }
    }
}
