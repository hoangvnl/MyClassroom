using MyClassroom.Contracts;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task<User> CreateAsync(User user, string password, UserRoles role = UserRoles.User);
        Task<bool> PasswordSignInAsync(string userName, string password);
    }
}
