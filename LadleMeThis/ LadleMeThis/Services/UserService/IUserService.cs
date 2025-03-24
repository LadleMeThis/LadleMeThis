using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Models.UserModels;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<IEnumerable<UserReviewDTO>> GetAllUsersInReviewFormatAsync();
        Task<UserResponseDTO> GetUserByIdAsync(string id);
        Task<IdentityResult> CreateUserAsync(UserDTO userDto);
        Task<IdentityResult> UpdateUserAsync(string id, UserDTO userDto);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<AuthResult> RegisterAsync(RegistrationRequest registerRequest, string role);
        Task<AuthResult> LoginAsync(AuthRequest authRequest);
    }
}
