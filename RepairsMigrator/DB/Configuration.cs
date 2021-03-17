using Microsoft.Extensions.Configuration;

namespace DB
{
    public static class Configuration
    {
        public static IConfiguration Instance { get; } = new ConfigurationBuilder()
            .AddUserSecrets<PropertyGateway>()
            .AddEnvironmentVariables()
            .Build();
    }
}
