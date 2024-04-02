using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MediatR;

namespace MyClassroom.Application.Queries
{
    public class LoginQuery : IRequest<BaseResponse<LoginResponse>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
