namespace LadleMeThis.Models.User
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
    }
}
