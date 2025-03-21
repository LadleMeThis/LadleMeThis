using Microsoft.EntityFrameworkCore;
using LadleMeThis.Context;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Repositories.SavedRecipeRepository;
using LadleMeThis.Models.UserModels;

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
                throw new Exception("Database error: Unable to fetch saved recipes.");
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
                throw new Exception("Database error: Unable to add saved recipe.");
            }
        }
        public async Task DeleteSavedRecipeAsync(int userId, int recipeId)
        {
            var savedRecipe = new SavedRecipe { UserId = userId, RecipeId = recipeId };

            _context.Entry(savedRecipe).State = EntityState.Deleted;

            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Saved recipe with User ID {userId} and Recipe ID {recipeId} not found.");
            }
        }
    }
}
