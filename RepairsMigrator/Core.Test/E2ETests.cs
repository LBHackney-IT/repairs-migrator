using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class E2ETests
    {
        [Fact]
        public async Task SimpleE2E()
        {
            const string valueFromDataProvider = "added from provider";
            const string valueFromInputModel = "added from in";
            var pipeline = new PipelineBuilder()
                .With(new TestDataProvider(valueFromDataProvider))
                .Build<TestOutModel>();

            var output = await pipeline.Run(new TestInModel
            {
                Value = valueFromInputModel
            });

            output.InputValue.Should().Be(valueFromInputModel);
            output.StageValue.Should().Be(valueFromDataProvider);
        }
    }

    internal class TestDataProvider : PipelineStage<TestStageModel>
    {
        private readonly string value;

        public TestDataProvider(string newValue)
        {
            this.value = newValue;
        }

        public override Task Process(TestStageModel model)
        {
            model.Value = this.value;
            return Task.CompletedTask;
        }
    }

    internal class TestStageModel
    {
        [PropertyKey(Keys.FromStage)]
        public string Value { get; set; }
    }

    internal class TestInModel
    {
        [PropertyKey(Keys.Input)]
        public string Value { get; set; }
    }

    internal class TestOutModel
    {
        [PropertyKey(Keys.FromStage)]
        public string StageValue { get; set; }

        [PropertyKey(Keys.Input)]
        public string InputValue { get; set; }
    }

    public static class Keys
    {
        public const string FromStage = "Value";
        public const string Input = "Input";
    }
}
