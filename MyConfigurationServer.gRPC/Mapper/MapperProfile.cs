using AutoMapper;
using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClassroomConfiguration, ConfigurationModel>()
                .ForMember(dest => dest.ClassroomId,
                           opt => opt.MapFrom(c => c.ClassroomId.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.ClassroomId,
                           opt => opt.MapFrom(c => ConvertStringToGuid(c.ClassroomId)));
        }

        private Guid ConvertStringToGuid(string guid)
        {
            Guid.TryParse(guid, out var returnValue);

            if (returnValue == Guid.Empty)
            {
                throw new ArgumentException(null, nameof(guid));
            }

            return returnValue;
        }
    }
}
