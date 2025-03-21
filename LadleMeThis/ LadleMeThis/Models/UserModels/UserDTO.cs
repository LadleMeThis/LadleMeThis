namespace LadleMeThis.Models.UserModels
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
    }
}
