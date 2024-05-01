using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Services
{
    public class ConfigurationService : Configuration.ConfigurationBase
    {
        private readonly ILogger<ConfigurationService> _logger;
        private readonly ClassroomConfigurationDatabaseSettings _dbSettings;
        private readonly IMapper _mapper;

        private readonly IMongoCollection<ClassroomConfiguration> _configurationCollection;

        public ConfigurationService(
            IOptions<ClassroomConfigurationDatabaseSettings> option,
            ILogger<ConfigurationService> logger,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;

            _dbSettings = option.Value;
            var settings = MongoClientSettings.FromConnectionString(_dbSettings.ConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(_dbSettings.DatabaseName);

            _configurationCollection = mongoDatabase.GetCollection<ClassroomConfiguration>(_dbSettings.ClassroomConfigurationsCollectionName);

        }

        public override async Task<BaseResponseInternal> CreateClassroomConfiguration(CreateClassroomConfigurationRequest request, ServerCallContext context)
        {
            try
            {
                if (Guid.TryParse(request.ClassroomId, out var classroomId))
                {
                    var config = await _configurationCollection.Find(x => x.ClassroomId == classroomId)?.FirstOrDefaultAsync();

                    if (config != null)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Configuration already exists",
                            Configuration = _mapper.Map<ClassroomConfigurationInternal>(config)
                        };
                    }
                    else
                    {
                        var obj = new ClassroomConfiguration()
                        {
                            ClassroomId = classroomId,
                            Color = request.Color,
                        };

                        await _configurationCollection.InsertOneAsync(obj);

                        return new()
                        {
                            IsSuccess = true,
                            Configuration = _mapper.Map<ClassroomConfigurationInternal>(obj)
                        };
                    }

                }
                else
                {
                    throw new FormatException($"Invalid Guid format: {request.ClassroomId}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<BaseResponseInternal> GetClassroomConfiguration(GetClassroomConfigurationRequest request, ServerCallContext context)
        {
            try
            {
                if (Guid.TryParse(request.ClassroomId, out var classroomId))
                {
                    var config = await _configurationCollection.Find(x => x.ClassroomId == classroomId)?.FirstOrDefaultAsync();

                    if (config != null)
                    {
                        return new()
                        {
                            IsSuccess = true,
                            Configuration = _mapper.Map<ClassroomConfigurationInternal>(config)
                        };
                    }
                    else
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Configuration = null,
                            Message = "Configuration not found"
                        };

                    }

                }
                else
                {
                    throw new FormatException($"Invalid Guid format: {request.ClassroomId}");
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
