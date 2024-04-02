namespace MyClassroom.Contracts
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public bool IsAuthSuccess { get; set; }
    }
}
