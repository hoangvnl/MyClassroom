using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyClassroom.Application.Extensions
{
    public static class MediatorExtension
    {
        public static void AddMediatRForLogic(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
