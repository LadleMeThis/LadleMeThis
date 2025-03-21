using LadleMeThis.Context;
using LadleMeThis.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly LadleMeThisContext _context;

        public UserRepository(LadleMeThisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to fetch users.");
            }
        }
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to fetch user by ID.");
            }
        }
        public async Task AddUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to create user.");
            }
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 0) throw new KeyNotFoundException($"User with ID {user.UserId} not found.");
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = new User { UserId = userId };

            _context.Entry(user).State = EntityState.Deleted;

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 0) throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
    }
}
