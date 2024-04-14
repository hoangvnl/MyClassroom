using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Contracts;
using MyClassroom.Contracts.EFCoreFilter;
using System.Security.Cryptography;
using System.Text;
using MyClassroom.Infrastructure.Helper;
using System.Linq.Expressions;

namespace MyClassroom.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User, UserDTO, Guid>(dbContext), IUserRepository
    {
        protected override IEnumerable<Expression<Func<User, object>>> GetAllRelatedObjects()
        {
            return new Expression<Func<User, object>>[]
            {
                user => user.Role
            };
        }

        public async Task<User> CreateAsync(User user, string password, Roles role = Roles.User)
        {
            try
            {
                var salt = GenSaltHelper.GenerateSalt();
                var hashedPassword = HashPassword(password, salt);

                user.Salt = salt;
                user.HashedPassword = hashedPassword;

                var result = await base.CreateAsync(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            EFCoreFilter<User> eFCoreFilter = new()
            {
                Filters = user => user.IsDeleted == false && user.Email.Equals(email)
            };

            var user = await base.GetAsync(eFCoreFilter);
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            EFCoreFilter<User> eFCoreFilter = new()
            {
                Filters = user => user.IsDeleted == false && user.Id == id
            };

            var user = await base.GetAsync(eFCoreFilter);
            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            EFCoreFilter<User> eFCoreFilter = new()
            {
                Filters = user => user.IsDeleted == false && user.UserName.Equals(userName)
            };

            var user = await base.GetAsync(eFCoreFilter);
            return user;
        }

        public async Task<User?> PasswordSignInAsync(string userName, string password)
        {
            EFCoreFilter<User> eFCoreFilter = new()
            {
                Filters = user => user.IsDeleted == false && user.UserName.Equals(userName)
            };

            var storedUser = await base.GetAsync(eFCoreFilter);

            if (storedUser == null) return null;

            string storedHashedPassword = storedUser.HashedPassword;
            var storedSaltBytes = storedUser.Salt;

            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(password);

            // Concatenate entered password and stored salt
            byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
            Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
            Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

            // Hash the concatenated value
            string enteredPasswordHash = HashPassword(password, storedSaltBytes);

            // Compare the entered password hash with the stored hash
            if (enteredPasswordHash == storedHashedPassword)
            {
                return storedUser;
            }
            else
            {
                return null;
            }

        }

        private static string HashPassword(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
    }
}
