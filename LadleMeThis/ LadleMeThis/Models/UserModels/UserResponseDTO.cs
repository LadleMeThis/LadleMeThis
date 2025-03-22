namespace LadleMeThis.Models.UserModels
{
    public class UserResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
