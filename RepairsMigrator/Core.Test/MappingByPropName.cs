using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class MappingByPropName
    {
        [Fact]
        public async Task SimpleE2E()
        {
            const string valueFromInputModel = "added from in";
            var pipeline = new PipelineBuilder()
                .Build<PropNameTestOutModel>();

            var output = await pipeline.Run(new PropNameTestInModel
            {
                Value = valueFromInputModel
            });

            output.Value.Should().Be(valueFromInputModel);
        }
    }

    [MapPropName]
    internal class PropNameTestOutModel
    {
        public string Value { get; set; }
    }

    internal class PropNameTestInModel
    {
        [MapPropName]
        public string Value { get; set; }
    }
}
