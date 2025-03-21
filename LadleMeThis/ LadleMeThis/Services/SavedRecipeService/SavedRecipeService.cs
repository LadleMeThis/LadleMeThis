using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Repositories.SavedRecipeRepository;

namespace LadleMeThis.Services.SavedRecipeService
{
    public class SavedRecipeService : ISavedRecipeService
    {
        private readonly SavedRecipeRepository _repository;

        public SavedRecipeService(SavedRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SavedRecipeResponseDTO>> GetUserSavedRecipesAsync(int userId)
        {

            var savedRecipes = await _repository.GetUserSavedRecipesAsync(userId);

            return savedRecipes.Select(sr => new SavedRecipeResponseDTO
            {
                RecipeName = sr.Recipe?.Name,
                DateSaved = sr.DateSaved
            }).ToList();
        }

        public async Task<SavedRecipeResponseDTO?> SaveRecipeAsync(int userId, int recipeId)
        {
            var savedRecipe = new SavedRecipe
            {
                UserId = userId,
                RecipeId = recipeId,
                DateSaved = DateTime.UtcNow
            };

            await _repository.AddSavedRecipeAsync(savedRecipe);

            return new SavedRecipeResponseDTO
            {
                RecipeName = savedRecipe.Recipe.Name,
                DateSaved = savedRecipe.DateSaved
            };
        }

        public async Task DeleteSavedRecipeAsync(int userId, int recipeId)
        {
            await _repository.DeleteSavedRecipeAsync(userId, recipeId);
        }
    }
}
