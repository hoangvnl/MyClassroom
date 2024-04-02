using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MediatR;

namespace MyClassroom.Application.Commands
{
    public class RegisterCommand : IRequest<BaseResponse<RegisterResponse>>
    {
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRoles Role { get; set; } = UserRoles.User;

        public RegisterCommand(string email, string password, string firstName, string lastName, string userName, UserRoles role = UserRoles.User)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Role = role;
        }

    }
}
