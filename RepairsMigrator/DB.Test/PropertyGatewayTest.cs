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
            var propGateway = new PropertyGateway();

            var result = await propGateway.GetPropertyReferences(new List<string>
            {
                "address_test"
            });

            result.First().Key.Should().Be("address_test");
        }
    }
}
