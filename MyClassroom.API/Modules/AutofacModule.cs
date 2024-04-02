using Autofac;
using MyClassroom.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyClassroom.Infrastructure;
using Microsoft.AspNetCore.Identity;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Infrastructure.Repositories;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.API.Modules
{
    public class AutofacModule : Module
    {
        private readonly IConfiguration configuration;

        public AutofacModule(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            builder.RegisterType<UserManager<ApplicationUser>>()
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<SignInManager<ApplicationUser>>()
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoleManager<IdentityRole<Guid>>>()
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserContextBuilder>()
                .As<IUserContextBuilder>()
                .InstancePerLifetimeScope();
            //builder.RegisterType<ClassroomProblemDetailsFactory>()
            //    .As<ProblemDetailsFactory>()
            //    .InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ClassroomRepository>()
                .As<IClassroomRepository>()
                .InstancePerLifetimeScope();            
            builder.RegisterType<UserClassroomRepository>()
                .As<IUserClassroomRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
