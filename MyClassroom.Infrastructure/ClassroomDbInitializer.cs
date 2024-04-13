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

    private void SeedUsersAndRoles(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

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

        if (context.Roles.Any(x => x.Name == UserRoles.Administrator.ToString())) return;

        roleManager.CreateAsync(new Role() { Name = UserRoles.Administrator.ToString() }).GetAwaiter().GetResult();
        roleManager.CreateAsync(new Role() { Name = UserRoles.User.ToString() }).GetAwaiter().GetResult();

        userManager.CreateAsync(new User
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
        }, "Admin123!").GetAwaiter().GetResult();

        User user = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
        userManager.AddToRoleAsync(user, UserRoles.Administrator.ToString()).GetAwaiter().GetResult();
    }

    private async Task SeedUserClassroomJoinTypeAsync(ApplicationDbContext context)
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
