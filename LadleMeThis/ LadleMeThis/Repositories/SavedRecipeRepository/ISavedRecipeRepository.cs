using LadleMeThis.Models.RecipeModels;

namespace LadleMeThis.Repositories.SavedRecipeRepository
{
    public interface ISavedRecipeRepository
    {
        Task<SavedRecipe?> GetSavedRecipeAsync(int userId, int recipeId);
        Task<List<SavedRecipe>> GetUserSavedRecipesAsync(int userId);
        Task AddSavedRecipeAsync(SavedRecipe savedRecipe);
        Task DeleteSavedRecipeAsync(SavedRecipe savedRecipe);
    }
}
