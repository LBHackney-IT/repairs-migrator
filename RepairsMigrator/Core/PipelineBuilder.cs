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


        public PipelineBuilder With(IBatchPipelineStage stage)
        {
            this.stages.Add(stage);
            return this;
        }

        public Pipeline<T> Build<T>()
            where T : class, new()
        {
            return new Pipeline<T>(stages);
        }
    }
}
