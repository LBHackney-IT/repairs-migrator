using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    class ResolveLevelCodeStage : PipelineStage<LevelCodeModel>
    {
        public override Task Process(LevelCodeModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LevelCode))
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }

    internal class LevelCodeModel
    {
        [PropertyKey(Keys.Level_Code)]
        public string LevelCode { get; set; }

        [PropertyKey(Keys.Level_Code_String)]
        public string LevelCodeString { get; set; }
    }
}
