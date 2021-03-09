using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class ErrorLogging
    {
        [Fact]
        public async Task LogsErrors()
        {
            var pipeline = new PipelineBuilder()
                .With(new ValidateStage<ErrorTestInModel>())
                .Build<ErrorTestOutModel>();

            var output = await pipeline.Run(new ErrorTestInModel());

            output.Errors.Should().Contain(e => e == "property must have value");
        }

        [Fact]
        public async Task NoErrors()
        {
            var pipeline = new PipelineBuilder()
                .With(new ValidateStage<ErrorTestInModel>())
                .Build<ErrorTestOutModel>();

            var output = await pipeline.Run(new ErrorTestInModel() { Value = "a value" });

            output.Errors.Should().BeEmpty();
        }
    }

    internal class ErrorTestInModel
    {
        [PropertyKey(Keys.Input)]
        [Required(ErrorMessage = "property must have value")]
        public string Value { get; set; }
    }

    internal class ErrorTestOutModel : IHasErrors
    {
        [PropertyKey(Keys.Input)]
        public string InputValue { get; set; }
        public IList<string> Errors { get; set; }
    }
}
