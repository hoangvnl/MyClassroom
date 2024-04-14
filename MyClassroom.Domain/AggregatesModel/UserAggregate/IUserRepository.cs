using MyClassroom.Contracts;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task<User> CreateAsync(User user, string password, Roles role = Roles.User);
        Task<User?> PasswordSignInAsync(string userName, string password);
    }
}
