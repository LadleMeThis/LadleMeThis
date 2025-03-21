using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Repositories.SavedRecipeRepository
{
    public interface ISavedRecipeRepository
    {
        Task<List<SavedRecipe>> GetUserSavedRecipesAsync(int userId);
        Task AddSavedRecipeAsync(SavedRecipe savedRecipe);
        Task DeleteSavedRecipeAsync(int userId, int recipeId);
    }
}
