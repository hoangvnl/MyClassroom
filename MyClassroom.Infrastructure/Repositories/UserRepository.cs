using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.SeedWork;
using MyClassroom.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MyClassroom.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return (IUnitOfWork)_dbContext;
            }
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password, UserRoles role = UserRoles.User)
        {
            using var transition = _dbContext.Database.BeginTransaction();

            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var test = result.Errors;
                    var errors = result.Errors.Select(e => e.Description);

                    throw new Exception(string.Join(" ", errors));
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role.ToString());

                if (!roleResult.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new Exception(string.Join(" ", errors));
                }

                transition.Commit();
                return true;
            }
            catch (Exception)
            {
                transition.Rollback();
                throw;
            }
        }

        public Task<ApplicationUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }
    }
}
