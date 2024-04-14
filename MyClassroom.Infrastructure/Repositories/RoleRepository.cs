using MyClassroom.Contracts;
using MyClassroom.Contracts.EFCoreFilter;
using MyClassroom.Domain.AggregatesModel.RoleAggregate;

namespace MyClassroom.Infrastructure.Repositories
{
    public class RoleRepository(ApplicationDbContext dbContext) : BaseRepository<Role, RoleDTO, Guid>(dbContext), IRoleRepository
    {

        public async Task<Role> GetByIdAsync(Guid id)
        {
            EFCoreFilter<Role> eFCoreFilter = new()
            {
                Filters = role => role.IsDeleted == false && role.Id == id
            };

            return await base.GetAsync(eFCoreFilter);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            EFCoreFilter<Role> eFCoreFilter = new()
            {
                Filters = role => role.IsDeleted == false && role.Name.Equals(name)
            };

            return await base.GetAsync(eFCoreFilter);
        }
    }
}
