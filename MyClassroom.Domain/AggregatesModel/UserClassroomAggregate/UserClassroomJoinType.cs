using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Domain.AggregatesModel.UserClassroomAggregate
{
    public class UserClassroomJoinType : Enumeration
    {
        public static UserClassroomJoinType Student = new(1, nameof(Student));
        public static UserClassroomJoinType Teacher = new(2, nameof(Teacher));

        public UserClassroomJoinType(int id, string name) : base(id, name)
        {
        }
    }
}
