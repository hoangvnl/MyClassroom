namespace MyConfigurationServer.gRPC.Clients
{
    public interface IConfigurationClient
    {
        public Task<ConfigurationModel> GetClassroomConfigurationAsync(Guid classId);
        public Task<ConfigurationModel> UpdateClassroomConfigurationAsync(UpdateConfigurationRequest updateConfigurationRequest);
    }
}
