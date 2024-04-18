using Grpc.Core;

namespace MyConfigurationServer.gRPC.Services
{
    public class ConfigurationService(ILogger<ConfigurationService> logger) : Configuration.ConfigurationBase
    {
        private readonly ILogger<ConfigurationService> _logger = logger;

        public override Task<ConfigurationModel> GetClassConfiguration(ConfigurationLookupModel request, ServerCallContext context)
        {
            ConfigurationModel returnValue = new();

            returnValue.Color = "Red";

            return Task.FromResult(returnValue);
        }

        public override Task<ConfigurationModel> UpdateClassConfiguration(UpdateConfigurationRequest request, ServerCallContext context)
        {
            ConfigurationModel returnValue = new();

            returnValue.Color = "White";

            return Task.FromResult(returnValue);
        }

    }
}
