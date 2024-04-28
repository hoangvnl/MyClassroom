using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Clients
{
    public interface IConfigurationClient
    {
        public Task<ConfigurationModel> GetClassroomConfigurationAsync(Guid classId);
        public Task<ConfigurationModel> UpdateClassroomConfigurationAsync(UpdateConfigurationRequest updateConfigurationRequest);
        public Task<ConfigurationModel> CreateClassroomConfigurationAsync(ClassroomConfiguration classroomConfiguration);
    }
}
