using Microsoft.AspNetCore.Identity;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Classroom> Classrooms { get; } = [];
        public List<UserClassroom> UserClassrooms { get; } = [];
        public bool IsDelete { get; set; } = true;

    }
}
