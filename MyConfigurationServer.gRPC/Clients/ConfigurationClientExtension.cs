using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using MyClassroom.Contracts;
using MyConfigurationServer.gRPC.Helpers;
using System.Net;

namespace MyConfigurationServer.gRPC.Clients
{
    public static class ConfigurationClientExtension
    {
        public static void RegisterClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationClientOptions>(configuration.GetSection("ConfigurationClientOptions"));
            services.Configure<APISettings>(APISettings.gRPCConfiguration, configuration.GetSection("APISettings:gRPCConfiguration"));
            services.AddScoped<ITokenProvider, AppTokenProvider>();
            services.AddSingleton<IConfigurationClient, ConfigurationClient>();
        }
    }
}
