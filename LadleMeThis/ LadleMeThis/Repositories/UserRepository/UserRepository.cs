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
                throw new Exception("Database error: Unable to fetch users.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user == null) throw new KeyNotFoundException($"User with ID {id} not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to fetch user by ID.", ex);
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
                throw new Exception("Database error: Unable to create user.", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to update user.", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = new User { UserId = id };

                _context.Entry(user).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to delete user.", ex);
            }
        }
    }
}
