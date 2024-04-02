using MediatR;
using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Infrastructure.Services;

namespace MyClassroom.Application.Queries
{
    public class GetAllClassroomQuery : IRequest<BaseResponse<List<ClassroomDto>>>
    {
        public UserContext UserContext { get; set; }

        public GetAllClassroomQuery(UserContext userContext)
        {
            UserContext = userContext;
        }
    }
}
