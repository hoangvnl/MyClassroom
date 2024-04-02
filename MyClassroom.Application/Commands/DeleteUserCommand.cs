using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Infrastructure.Services;

namespace MyClassroom.Application.Commands
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public Guid UserId { get; set; }
        public UserContext UserContext { get; set; }

        public DeleteUserCommand(Guid userId, UserContext userContext)
        {
            UserId = userId;
            UserContext = userContext;
        }
    }
}
