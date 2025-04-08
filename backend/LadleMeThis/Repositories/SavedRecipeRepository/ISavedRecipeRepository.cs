using LadleMeThis.Data.Entity;

namespace LadleMeThis.Repositories.SavedRecipeRepository
{
    public interface ISavedRecipeRepository
    {
        Task<List<SavedRecipe>> GetUserSavedRecipesAsync(string userId);
        Task AddSavedRecipeAsync(SavedRecipe savedRecipe);
        Task DeleteSavedRecipeAsync(string userId, int recipeId);
    }
}
