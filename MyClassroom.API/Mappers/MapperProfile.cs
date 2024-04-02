using AutoMapper;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClassroomDto, Classroom>().ReverseMap();

            CreateMap<UserClassroom, UserJoinClassroomResponse>()
                .ForMember(dest => dest.UserClassroomJoinType,
                           opt => opt.MapFrom(c => (UserJoinClassroomTypes)c.UserClassroomJoinTypeId));
        }
    }
}
