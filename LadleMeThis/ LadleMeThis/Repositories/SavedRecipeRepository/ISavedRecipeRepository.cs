using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Repositories.SavedRecipeRepository
{
    public interface ISavedRecipeRepository
    {
        Task<List<SavedRecipe>> GetUserSavedRecipesAsync(string userId);
        Task AddSavedRecipeAsync(SavedRecipe savedRecipe);
        Task DeleteSavedRecipeAsync(string userId, int recipeId);
    }
}
