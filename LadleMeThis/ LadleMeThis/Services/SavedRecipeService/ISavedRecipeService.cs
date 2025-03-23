using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Services.SavedRecipeService
{
    public interface ISavedRecipeService
    {
        Task<SavedRecipeResponseDTO?> SaveRecipeAsync(string userId, int recipeId);
        Task<List<SavedRecipeResponseDTO>> GetUserSavedRecipesAsync(string userId);
        Task DeleteSavedRecipeAsync(string userId, int recipeId);
    }
}
