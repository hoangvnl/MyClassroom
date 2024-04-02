using System.Security.Claims;

namespace MyClassroom.Infrastructure.Services
{
    public interface IUserContextBuilder
    {
        UserContext BuildUserContext(ClaimsIdentity claims);
    }
}
