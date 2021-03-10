﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IBatchPipelineStage
    {
        Task Process(IEnumerable<PropertyBag> bag);
    }

    class BatchWrapper : IBatchPipelineStage
    {
        private readonly IPipelineStage stage;

        public BatchWrapper(IPipelineStage stage)
        {
            this.stage = stage;
        }

        public async Task Process(IEnumerable<PropertyBag> bags)
        {
            foreach (var item in bags)
            {
                await stage.Process(item);
            }
        }
    }
}
