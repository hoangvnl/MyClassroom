using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Infrastructure.Services;

namespace MyClassroom.Application.Commands
{
    public class CreateClassroomCommand : IRequest<BaseResponse<ClassroomDto>>
    {
        public CreateClassroomRequest CreateClassroomRequest { get; }
        public UserContext UserContext { get; }

        public CreateClassroomCommand() { }

        public CreateClassroomCommand(CreateClassroomRequest createClassroomRequest, UserContext userContext)
        {
            CreateClassroomRequest = createClassroomRequest;
            UserContext = userContext;
        }
    }
}
