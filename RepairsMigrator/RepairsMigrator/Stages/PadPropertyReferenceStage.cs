using Core;
using Serilog;
using System.Threading.Tasks;

namespace RepairsMigrator.Stages
{
    public class PadPropertyReferenceStage : PipelineStage<PropRefPad>
    {
        public override Task Startup()
        {
            Log.Information("Normalising property references");
            return base.Startup();
        }

        public override Task Process(PropRefPad model)
        {
            model.PropRef = model.PropRef?.ToLowerInvariant().Trim();

            if (string.IsNullOrWhiteSpace(model.PropRef)) return Task.CompletedTask;

            if (!int.TryParse(model.PropRef, out _)) return Task.CompletedTask;

            model.PropRef = model.PropRef.PadLeft(8, '0');

            return Task.CompletedTask;
        }
    }

    public class PropRefPad
    {
        [PropertyKey(Keys.Property_Reference)]
        public string PropRef { get; set; }
    }
}
