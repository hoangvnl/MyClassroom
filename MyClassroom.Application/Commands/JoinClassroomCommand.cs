using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Infrastructure.Services;

namespace MyClassroom.Application.Commands
{
    public class JoinClassroomCommand : IRequest<BaseResponse<UserJoinClassroomResponse>>
    {
        public UserJoinClassroomRequest JoinClassroomRequest { get; set; }
        public UserContext UserContext { get; set; }

        public JoinClassroomCommand(UserJoinClassroomRequest joinClassroomRequest, UserContext userContext)
        {
            JoinClassroomRequest = joinClassroomRequest;
            UserContext = userContext;

        }
    }
}
