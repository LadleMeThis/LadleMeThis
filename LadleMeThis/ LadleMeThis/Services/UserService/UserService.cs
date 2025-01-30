using LadleMeThis.Models.User;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                DisplayName = user.DisplayName,
                DateCreated = user.DateCreated
            }).ToList();
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                DisplayName = user.DisplayName,
                DateCreated = user.DateCreated,
            };
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, userDto.PasswordHash),
                DisplayName = userDto.DisplayName,
                DateCreated = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.DisplayName = userDto.DisplayName;
            user.PasswordHash = _passwordHasher.HashPassword(null, userDto.PasswordHash);

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

    }
}
