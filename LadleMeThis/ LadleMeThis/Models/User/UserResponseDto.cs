namespace LadleMeThis.Models.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
