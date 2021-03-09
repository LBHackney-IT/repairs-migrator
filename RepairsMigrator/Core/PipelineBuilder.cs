using System.Collections.Generic;

namespace Core
{
    public class PipelineBuilder
    {
        private readonly IList<IPipelineStage> stages;

        public PipelineBuilder()
        {
            stages = new List<IPipelineStage>();
        }

        public PipelineBuilder With(IPipelineStage stage)
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
