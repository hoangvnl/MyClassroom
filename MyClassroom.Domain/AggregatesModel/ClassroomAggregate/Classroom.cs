using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.ClassroomAggregate
{
    public class Classroom : Entity<Guid>, IAggregateRoot
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ApplicationUser> Users { get; } = [];
        public List<UserClassroom> UserClassrooms { get; } = [];

        protected Classroom()
        {
        }

        public Classroom(string title, string description, Guid userId) : base(userId)
        {
            Title = title;
            Description = description;
        }
    }
}
