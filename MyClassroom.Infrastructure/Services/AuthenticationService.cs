using MyClassroom.Domain.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MyClassroom.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly APISettings _apiSettings;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IOptions<APISettings> options, IUserRepository userRepository)
        {
            _apiSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("UserId", user.Id.ToString()),
                new Claim("RoleName", user.Role.Name)
            };
            //var roles = await _userManager.GetRolesAsync(user);

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            return await Task.FromResult(claims);
        }

        public SigningCredentials GetSignCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
