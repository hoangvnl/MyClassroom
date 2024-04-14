using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.RoleAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get;set; } = string.Empty;
        public string HashedPassword { get; set;} = string.Empty;
        public byte[] Salt { get; set; }

        public List<Classroom> Classrooms { get; } = [];
        public List<UserClassroom> UserClassrooms { get; } = [];

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
