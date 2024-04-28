
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Clients
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly Configuration.ConfigurationClient _configurationgRPCClient;
        private readonly ConfigurationClientOptions _options;

        public ConfigurationClient(ConfigurationClientOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            GrpcChannel channel = GrpcChannel.ForAddress(_options.BaseUrl);
            _configurationgRPCClient = new(channel);
        }

        public async Task<ConfigurationModel> CreateClassroomConfigurationAsync(ClassroomConfiguration classroomConfiguration)
        {
            //var returnValue = await _configurationgRPCClient.CreateClassroomConfigurationAsync(
            //    new()
            //    {
            //        ClassroomId = classroomConfiguration.ClassroomId,

            //    }
            //    );
            throw new NotImplementedException();

        }

        public async Task<ConfigurationModel> GetClassroomConfigurationAsync(Guid classroomId)
        {
            var returnValue = await _configurationgRPCClient.GetClassroomConfigurationAsync(
                new ConfigurationLookupModel
                {
                    ClassroomId = classroomId.ToString()
                });

            return returnValue;
        }

        public Task<ConfigurationModel> UpdateClassroomConfigurationAsync(UpdateConfigurationRequest updateConfigurationRequest)
        {
            throw new NotImplementedException();
        }
    }
}
