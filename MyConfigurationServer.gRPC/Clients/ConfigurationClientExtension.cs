using AutoMapper;

namespace MyConfigurationServer.gRPC.Clients
{
    public static class ConfigurationClientExtension
    {
        public static void RegisterClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationClientOptions>(configuration.GetSection("ConfigurationClientOptions"));
            services.AddSingleton<IConfigurationClient, ConfigurationClient>();
        }
    }
}
