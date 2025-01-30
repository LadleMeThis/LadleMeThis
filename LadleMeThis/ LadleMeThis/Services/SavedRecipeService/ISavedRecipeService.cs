using LadleMeThis.Models.SavedRecipe;

namespace LadleMeThis.Services.SavedRecipeService
{
    public interface ISavedRecipeService
    {
        Task<SavedRecipeResponseDto?> SaveRecipeAsync(int userId, int recipeId);
        Task<List<SavedRecipeResponseDto>> GetUserSavedRecipesAsync(int userId);
        Task<bool> DeleteSavedRecipeAsync(int userId, int recipeId);
    }
}
