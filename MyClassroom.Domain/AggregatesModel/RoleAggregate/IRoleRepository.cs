using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.RoleAggregate
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetByUserIdAsync(Guid userId);
    }
}
