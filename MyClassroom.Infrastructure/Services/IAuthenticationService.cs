using MyClassroom.Domain.AggregatesModel.UserAggregate;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MyClassroom.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        SigningCredentials GetSignCredentials();
        Task<List<Claim>> GetClaimsAsync(User user);
    }
}
