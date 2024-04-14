using MyClassroom.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Domain.SeedWork;
using MyClassroom.Domain.AggregatesModel.RoleAggregate;

namespace MyClassroom.Infrastructure;

public class ClassroomDbInitializer
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        SeedUsersAndRoles(context, serviceProvider);
        await SeedUserClassroomJoinTypeAsync(context);
    }

    private static void SeedUsersAndRoles(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        var roleRepository = serviceProvider.GetRequiredService<IRoleRepository>();

        try
        {
            if (context.Database.GetPendingMigrations().Count() > 0)
            {
                context.Database.Migrate();
            }
        }
        catch (Exception)
        {

            throw;
        }

        if (context.Roles.Any(x => x.Name == Roles.Administrator.ToString())) return;

        var adminRole = roleRepository.CreateAsync(new Role() { Name = Roles.Administrator.ToString() }).GetAwaiter().GetResult();
        var userRole = roleRepository.CreateAsync(new Role() { Name = Roles.User.ToString() }).GetAwaiter().GetResult();

        userRepository.CreateAsync(new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            RoleId = adminRole.Id
        }, "Admin123!").GetAwaiter().GetResult();

        User user = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
    }

    private static async Task SeedUserClassroomJoinTypeAsync(ApplicationDbContext context)
    {
        using (context)
        {
            context.Database.Migrate();

            if (!context.UserClassroomJoinTypes.Any())
            {
                context.UserClassroomJoinTypes.AddRange(Enumeration.GetAll<UserClassroomJoinType>());

                await context.SaveChangesAsync();

            }
        }
    }
}
