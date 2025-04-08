using LadleMeThis.Data.Entity;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Repositories.SavedRecipeRepository;

namespace LadleMeThis.Services.SavedRecipeService
{
    public class SavedRecipeService(SavedRecipeRepository repository) : ISavedRecipeService
    {
        public async Task<List<SavedRecipeResponseDTO>> GetUserSavedRecipesAsync(string userId)
        {

            var savedRecipes = await repository.GetUserSavedRecipesAsync(userId);

            return savedRecipes.Select(sr => new SavedRecipeResponseDTO
            {
                RecipeName = sr.Recipe?.Name,
                DateSaved = sr.DateSaved
            }).ToList();
        }

        public async Task<SavedRecipeResponseDTO?> SaveRecipeAsync(string userId, int recipeId)
        {
            var savedRecipe = new SavedRecipe
            {
                UserId = userId,
                RecipeId = recipeId,
                DateSaved = DateTime.UtcNow
            };

            await repository.AddSavedRecipeAsync(savedRecipe);

            return new SavedRecipeResponseDTO
            {
                RecipeName = savedRecipe.Recipe.Name,
                DateSaved = savedRecipe.DateSaved
            };
        }

        public async Task DeleteSavedRecipeAsync(string userId, int recipeId)
        {
            await repository.DeleteSavedRecipeAsync(userId, recipeId);
        }
    }
}
