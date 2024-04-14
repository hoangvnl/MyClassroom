using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MediatR;

namespace MyClassroom.Application.Commands
{
    public class RegisterCommand(string email, string password, string firstName, string lastName, string userName, Roles role = Roles.User) : IRequest<BaseResponse<RegisterResponse>>
    {
        public string UserName { get; set; } = userName;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Roles Role { get; set; } = role;
    }
}
