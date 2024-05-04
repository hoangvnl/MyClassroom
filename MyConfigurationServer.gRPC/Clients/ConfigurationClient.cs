using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using MyConfigurationServer.gRPC.Contracts;
using MyConfigurationServer.gRPC.Helpers;

namespace MyConfigurationServer.gRPC.Clients
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly Configuration.ConfigurationClient _client;
        private readonly ConfigurationClientOptions _options;
        private readonly IMapper _mapper;

        public ConfigurationClient(IOptions<ConfigurationClientOptions> options, IMapper mapper, ITokenProvider tokenProvider)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var token = tokenProvider.GetToken();
            GrpcChannel channel = CreateAuthenticatedChannel(_options.BaseUrl, token);
            _client = new(channel);
        }

        private static GrpcChannel CreateAuthenticatedChannel(string baseUrl, string token)
        {
            var credentials = CallCredentials.FromInterceptor(async (context, metadata) =>
            {
                await Task.Run(() =>
                {
                    metadata.Add("Authorization", $"Bearer {token}");

                });
            });

            var channel = GrpcChannel.ForAddress(baseUrl, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });

            return channel;
        }

        public async Task<BaseResponse<ClassroomConfiguration>> CreateClassroomConfigurationAsync(ClassroomConfiguration classroomConfiguration)
        {
            var request = new CreateClassroomConfigurationRequest()
            {
                ClassroomId = classroomConfiguration.Id,
                Color = classroomConfiguration.Color
            };

            var config = await _client.CreateClassroomConfigurationAsync(request);
            return _mapper.Map<BaseResponse<ClassroomConfiguration>>(config);
        }

        public async Task<BaseResponse<ClassroomConfiguration>> GetClassroomConfigurationAsync(Guid classId)
        {
            try
            {
                var request = new GetClassroomConfigurationRequest()
                {
                    ClassroomId = classId.ToString()
                };

                var config = await _client.GetClassroomConfigurationAsync(request);

                return new BaseResponse<ClassroomConfiguration>(_mapper.Map<ClassroomConfiguration>(config.Configuration));
            }
            catch (RpcException ex)
            {
                return new BaseResponse<ClassroomConfiguration>(RpcErrorHandler.GetErrorDetail(ex.StatusCode));
            }
        }

        public Task<BaseResponse<ClassroomConfiguration>> UpdateClassroomConfigurationAsync(ClassroomConfiguration updateConfigurationRequest)
        {
            throw new NotImplementedException();
        }
    }
}
