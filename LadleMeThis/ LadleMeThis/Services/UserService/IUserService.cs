using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<IEnumerable<UserReviewDTO>> GetAllUsersInReviewFormatAsync();
        Task<UserResponseDTO> GetUserByIdAsync(int id);
        Task CreateUserAsync(UserDTO userDto);
        Task UpdateUserAsync(int id, UserDTO userDto);
        Task DeleteUserAsync(int id);
    }
}
