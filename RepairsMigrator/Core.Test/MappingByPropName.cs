using FluentAssertions;
using System.Linq;
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
            var pipeline = new PipelineBuilder().Build();

            pipeline.In(Helpers.List(new PropNameTestInModel
            {
                Value = valueFromInputModel
            }));

            await pipeline.Run();

            var output = pipeline.Out<PropNameTestOutModel>();

            output.Single().Value.Should().Be(valueFromInputModel);
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
