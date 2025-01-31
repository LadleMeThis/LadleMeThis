using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Services.SavedRecipeService
{
    public interface ISavedRecipeService
    {
        Task<SavedRecipeResponseDto?> SaveRecipeAsync(int userId, int recipeId);
        Task<List<SavedRecipeResponseDto>> GetUserSavedRecipesAsync(int userId);
        Task DeleteSavedRecipeAsync(int userId, int recipeId);
    }
}
