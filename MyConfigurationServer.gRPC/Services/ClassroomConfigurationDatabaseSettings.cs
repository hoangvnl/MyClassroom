namespace MyConfigurationServer.gRPC.Services
{
    public class ClassroomConfigurationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ClassroomConfigurationsCollectionName { get; set; } = null!;
    }
}
