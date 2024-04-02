namespace MyClassroom.Contracts
{
    public class UserJoinClassroomResponse
    {
        public Guid UserId { get; set; }
        public Guid ClassroomId { get; set; }
        public UserJoinClassroomTypes UserClassroomJoinType { get; set; }
    }
}
