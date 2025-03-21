using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Services.SavedRecipeService
{
    public interface ISavedRecipeService
    {
        Task<SavedRecipeResponseDTO?> SaveRecipeAsync(int userId, int recipeId);
        Task<List<SavedRecipeResponseDTO>> GetUserSavedRecipesAsync(int userId);
        Task DeleteSavedRecipeAsync(int userId, int recipeId);
    }
}
