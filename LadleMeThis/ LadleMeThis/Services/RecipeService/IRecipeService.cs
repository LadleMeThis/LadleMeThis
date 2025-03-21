using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;

namespace LadleMeThis.Services.RecipeService;

public interface IRecipeService
{
	Task<List<RecipeCardDTO>> GetALlRecipeCards();
	Task<List<RecipeCardDTO>> GetRecipesByCategoryId(int categoryId);
	Task<List<RecipeCardDTO>> GetRecipesByTagId(int tagId);
	Task<List<RecipeCardDTO>> GetRecipesByIngredientId(int ingredientId);
	Task<FullRecipeDTO> GetRecipeByRecipeId(int recipeId, User user);
	Task<bool> DeleteRecipe(int recipeId);
	Task<int> Create(CreateRecipeDTO createRecipeDto, User user);
	Task<bool> UpdateRecipe(UpdateRecipeDTO updateRecipeDto);
}