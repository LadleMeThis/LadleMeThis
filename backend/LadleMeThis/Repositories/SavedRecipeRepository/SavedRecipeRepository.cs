using Microsoft.EntityFrameworkCore;
using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
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

        public async Task<List<SavedRecipe>> GetUserSavedRecipesAsync(string userId)
        {
            try
            {
                return await _context.SavedRecipes
                    .Where(sr => sr.User.Id == userId)
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
        public async Task DeleteSavedRecipeAsync(string userId, int recipeId)
        {
            var savedRecipe = await _context.SavedRecipes
                .FirstOrDefaultAsync(sr => sr.User.Id == userId && sr.Recipe.RecipeId == recipeId);

            if (savedRecipe == null)
                throw new KeyNotFoundException($"Saved recipe with User ID {userId} and Recipe ID {recipeId} not found.");

            _context.SavedRecipes.Remove(savedRecipe);
            await _context.SaveChangesAsync();
        }
    }
}
