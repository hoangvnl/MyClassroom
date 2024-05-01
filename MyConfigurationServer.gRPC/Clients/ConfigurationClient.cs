
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Clients
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly Configuration.ConfigurationClient _client;
        private readonly ConfigurationClientOptions _options;
        private readonly IMapper _mapper;

        public ConfigurationClient(IOptions<ConfigurationClientOptions> options, IMapper mapper)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            GrpcChannel channel = GrpcChannel.ForAddress(_options.BaseUrl);
            _client = new(channel);
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

                return new BaseResponse<ClassroomConfiguration>()
                {
                    IsSuccess = config.IsSuccess,
                    Message = config.Message,
                    Value = _mapper.Map<ClassroomConfiguration>(config.Configuration)
                };
            }
            catch (RpcException)
            {
                throw;
            }

        }

        public Task<BaseResponse<ClassroomConfiguration>> UpdateClassroomConfigurationAsync(ClassroomConfiguration updateConfigurationRequest)
        {
            throw new NotImplementedException();
        }
    }
}
