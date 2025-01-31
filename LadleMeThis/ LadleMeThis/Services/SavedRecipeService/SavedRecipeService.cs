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

        public async Task<List<SavedRecipeResponseDto>> GetUserSavedRecipesAsync(int userId)
        {

            var savedRecipes = await _repository.GetUserSavedRecipesAsync(userId);

            return savedRecipes.Select(sr => new SavedRecipeResponseDto
            {
                RecipeName = sr.Recipe?.Name,
                DateSaved = sr.DateSaved
            }).ToList();
        }

        public async Task<SavedRecipeResponseDto?> SaveRecipeAsync(int userId, int recipeId)
        {
            var savedRecipe = new SavedRecipe
            {
                UserId = userId,
                RecipeId = recipeId,
                DateSaved = DateTime.UtcNow
            };

            await _repository.AddSavedRecipeAsync(savedRecipe);

            return new SavedRecipeResponseDto
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
