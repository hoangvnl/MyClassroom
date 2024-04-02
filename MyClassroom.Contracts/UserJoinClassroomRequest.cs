namespace MyClassroom.Contracts
{
    public class UserJoinClassroomRequest
    {
        public Guid UserId { get; set; }
        public Guid ClassroomId { get; set; }
        public UserJoinClassroomTypes UserClassroomJoinType { get; set; }
    }
}
