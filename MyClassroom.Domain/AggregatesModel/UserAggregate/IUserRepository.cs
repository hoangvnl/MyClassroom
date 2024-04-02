using MyClassroom.Contracts;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> CreateAsync(ApplicationUser user, string password, UserRoles role = UserRoles.User);
    }
}
