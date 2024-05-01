using MyConfigurationServer.gRPC.Contracts;

namespace MyConfigurationServer.gRPC.Clients
{
    public interface IConfigurationClient
    {
        public Task<BaseResponse<ClassroomConfiguration>> GetClassroomConfigurationAsync(Guid classId);
        public Task<BaseResponse<ClassroomConfiguration>> UpdateClassroomConfigurationAsync(ClassroomConfiguration updateConfigurationRequest);
        public Task<BaseResponse<ClassroomConfiguration>> CreateClassroomConfigurationAsync(ClassroomConfiguration classroomConfiguration);
    }
}
