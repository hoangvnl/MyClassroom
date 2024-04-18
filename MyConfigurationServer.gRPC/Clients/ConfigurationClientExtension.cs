namespace MyConfigurationServer.gRPC.Clients
{
    public static class ConfigurationClientExtension
    {
        public static void RegisterClient(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("ConfigurationClientOptions");
            var configOptions = configSection.Get<ConfigurationClientOptions>();
            ConfigurationClient client = new(configOptions!);

            services.AddSingleton<IConfigurationClient>(client);
        }
    }
}
