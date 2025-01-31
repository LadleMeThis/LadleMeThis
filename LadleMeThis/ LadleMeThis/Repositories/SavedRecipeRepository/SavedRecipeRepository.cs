using Microsoft.EntityFrameworkCore;
using LadleMeThis.Context;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Repositories.SavedRecipeRepository;

namespace LadleMeThis.Repositories.SavedRecipeRepository
{
    public class SavedRecipeRepository : ISavedRecipeRepository
    {
        private readonly LadleMeThisContext _context;

        public SavedRecipeRepository(LadleMeThisContext context)
        {
            _context = context;
        }

        public async Task<List<SavedRecipe>> GetUserSavedRecipesAsync(int userId)
        {
            try
            {
                return await _context.SavedRecipes
                    .Where(sr => sr.UserId == userId)
                    .Include(sr => sr.Recipe)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to fetch saved recipes.", ex);
            }
        }
        public async Task<SavedRecipe?> GetSavedRecipeAsync(int userId, int recipeId)
        {
            try
            {
                return await _context.SavedRecipes
                    .FirstOrDefaultAsync(sr => sr.UserId == userId && sr.RecipeId == recipeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to retrieve saved recipe.", ex);
            }
        }
        public async Task AddSavedRecipeAsync(SavedRecipe savedRecipe)
        {
            try
            {
                _context.SavedRecipes.Add(savedRecipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to add saved recipe.", ex);
            }
        }
        public async Task DeleteSavedRecipeAsync(SavedRecipe savedRecipe)
        {
            try
            {
                _context.Entry(savedRecipe).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: Unable to delete saved recipe.", ex);
            }
        }
    }
}
