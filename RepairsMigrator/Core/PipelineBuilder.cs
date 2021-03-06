using System.Collections.Generic;

namespace Core
{
    public class PipelineBuilder
    {
        private readonly IList<IBatchPipelineStage> stages;

        public PipelineBuilder()
        {
            stages = new List<IBatchPipelineStage>();
        }

        public PipelineBuilder With(IPipelineStage stage)
        {
            this.stages.Add(new BatchWrapper(stage));
            return this;
        }

        public PipelineBuilder With(IFilter filter)
        {
            this.stages.Add(new FilterWrapper(filter));
            return this;
        }

        public PipelineBuilder With(IBatchPipelineStage stage)
        {
            this.stages.Add(stage);
            return this;
        }

        public Pipeline Build()
        {
            return new Pipeline(stages);
        }
    }
}
