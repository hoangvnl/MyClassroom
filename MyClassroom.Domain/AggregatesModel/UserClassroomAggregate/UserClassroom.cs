using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClassroom.Domain.AggregatesModel.UserClassroomAggregate
{
    public class UserClassroom : Entity<Guid>, IAggregateRoot
    {
        public Guid ClassroomId { get; protected set; }
        public Guid UserId { get; protected set; }
        public Classroom Classroom { get; protected set; } = null!;
        public User User { get; protected set; } = null!;

        public int UserClassroomJoinTypeId { get; protected set; }
        public UserClassroomJoinType UserClassroomJoinType { get; protected set; } = null!;

        public UserClassroom() { }

        public UserClassroom(Guid ClassroomId, Guid userId, UserClassroomJoinType joinType) : base(userId)
        {
            Id = Guid.NewGuid();
            ClassroomId = ClassroomId;
            UserId = userId;
            UserClassroomJoinTypeId = joinType.Id;
        }
    }
}
