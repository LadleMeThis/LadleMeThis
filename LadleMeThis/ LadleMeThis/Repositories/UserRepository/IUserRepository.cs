using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Repositories.UserRepository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);

}