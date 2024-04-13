using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.RoleAggregate
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; } = new List<User>();
    }
}
