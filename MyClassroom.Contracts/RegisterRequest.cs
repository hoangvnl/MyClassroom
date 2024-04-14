namespace MyClassroom.Contracts
{
    public class RegisterRequest(string userName, string password, string firstName, string lastName, string email, Roles role = Roles.User)
    {
        public string Email { get; set; } = email;
        public string UserName { get; set; } = userName;
        public string Password { get; set; } = password;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Roles Role { get; set; } = role;
    }
}
