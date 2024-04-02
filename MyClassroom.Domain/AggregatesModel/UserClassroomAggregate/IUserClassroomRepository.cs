using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserClassroomAggregate
{
    public interface IUserClassroomRepository : IRepository<UserClassroom>
    {
        Task<UserClassroom> CreateAsync(UserClassroom userClassroom);
        Task<IList<Guid>> GetAllUserIdsByClassroomIdAsync(Guid ClassroomId);
        Task<IList<Guid>> GetAllClassroomIdsByUserIdAsync(Guid userId);
    }
}
