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

        public override async Task<ConfigurationModel> GetClassroomConfiguration(ConfigurationLookupModel request, ServerCallContext context)
        {
            ConfigurationModel returnValue = new();

            try
            {
                Guid.TryParse(request.ClassroomId, out var classroomId);

                if (classroomId == Guid.Empty)
                {
                    throw new TypeAccessException($"{nameof(classroomId)}");
                }

                var result = await _configurationCollection.Find(x => x.ClassroomId == classroomId)?.FirstOrDefaultAsync();
                returnValue.Color = result?.Color ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }

            return returnValue;
        }

        public override Task<ConfigurationModel> UpdateClassroomConfiguration(UpdateConfigurationRequest request, ServerCallContext context)
        {
            ConfigurationModel returnValue = new();

            returnValue.Color = "White";

            return Task.FromResult(returnValue);
        }

        public override async Task<ConfigurationModel> CreateClassroomConfiguration(ConfigurationModel request, ServerCallContext context)
        {
            ConfigurationModel returnValue = request;

            try
            {
                Guid.TryParse(request.ClassroomId, out var classroomId);

                if (classroomId == Guid.Empty)
                {
                    throw new TypeAccessException($"{nameof(classroomId)}");
                }

                await _configurationCollection.InsertOneAsync(_mapper.Map<ClassroomConfiguration>(request));
            }
            catch (Exception)
            {
                throw;
            }

            return returnValue;
        }

    }
}
