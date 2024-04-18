
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace MyConfigurationServer.gRPC.Clients
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly Configuration.ConfigurationClient _configurationClient;
        private readonly ConfigurationClientOptions _options;

        public ConfigurationClient(ConfigurationClientOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            GrpcChannel channel = GrpcChannel.ForAddress(_options.BaseUrl);
            _configurationClient = new(channel);
        }

        public async Task<ConfigurationModel> GetClassroomConfigurationAsync(Guid classId)
        {
            var returnValue = await _configurationClient.GetClassConfigurationAsync(new ConfigurationLookupModel { ClassId = classId.ToString() });

            return returnValue;
        }

        public async Task<ConfigurationModel> UpdateClassroomConfigurationAsync(UpdateConfigurationRequest updateConfigurationRequest)
        {
            return await _configurationClient.UpdateClassConfigurationAsync(updateConfigurationRequest);
        }
    }
}
