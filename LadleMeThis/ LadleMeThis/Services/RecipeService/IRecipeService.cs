using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;

namespace LadleMeThis.Services.RecipeService;

public interface IRecipeService
{
	Task<List<RecipeCardDto>> GetALlRecipeCards();
	Task<List<RecipeCardDto>> GetRecipesByCategoryId(int categoryId);
	Task<List<RecipeCardDto>> GetRecipesByTagId(int tagId);
	Task<List<RecipeCardDto>> GetRecipesByIngredientId(int ingredientId);
	Task<FullRecipeDTO> GetRecipeByRecipeId(int recipeId, User user);
	Task<bool> DeleteRecipe(int recipeId);
	Task<int> Create(CreateRecipeDTO createRecipeDto, User user);
	Task<bool> UpdateRecipe(UpdateRecipeDto updateRecipeDto);
}