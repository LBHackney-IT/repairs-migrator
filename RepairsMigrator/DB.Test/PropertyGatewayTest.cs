using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DB.Test
{
    public class PropertyGatewayTest
    {
        [Fact]
        public async Task Test()
        {
            var result = await PropertyGateway.GetPropertyReferences();

            result.First().Key.Should().Be("address_test");
        }
    }
}
