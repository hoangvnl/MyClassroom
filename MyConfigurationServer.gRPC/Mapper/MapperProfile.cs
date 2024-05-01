using AutoMapper;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClassroomConfiguration, ClassroomConfigurationInternal>()
                .ForMember(dest => dest.ClassroomId,
                           opt => opt.MapFrom(c => c.ClassroomId.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.ClassroomId,
                           opt => opt.MapFrom(c => ConvertStringToGuid(c.ClassroomId)));

            CreateMap<BaseResponseInternal, BaseResponse<ClassroomConfiguration>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Configuration));

        }

        private static Guid ConvertStringToGuid(string guid)
        {
            if (Guid.TryParse(guid, out var result))
            {
                return result;
            }
            else
            {
                throw new FormatException($"Invalid Guid format in ClassroomConfigurationInternal.Id : {guid}");
            }
        }
    }
}
