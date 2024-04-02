using System.Security.Claims;

namespace MyClassroom.Infrastructure.Services
{
    public class UserContextBuilder : IUserContextBuilder
    {
        public UserContext BuildUserContext(ClaimsIdentity claims)
        {
            if (claims == null) throw new Exception("User not found");

            var userContext = new UserContext();

            Guid.TryParse(claims.FindFirst("UserId")?.Value ?? "", out var id);
            var name = claims.FindFirst(ClaimTypes.Name)?.Value;
            var email = claims.FindFirst(ClaimTypes.Email)?.Value;

            userContext.UserId = id;
            userContext.Name = name ?? string.Empty;
            userContext.Email = email ?? string.Empty;

            return userContext;
        }
    }
}
