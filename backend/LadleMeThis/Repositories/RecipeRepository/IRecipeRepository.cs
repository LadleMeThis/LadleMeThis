using LadleMeThis.Data.Entity;

namespace LadleMeThis.Repositories.RecipeRepository;

public interface IRecipeRepository
{
	Task<List<Recipe>> GetAll();
	Task<List<Recipe>> GetByCategoryId(int categoryId);
	Task<List<Recipe>> GetByName(string name);
    Task<List<Recipe>> GetByCategoryIdAndName(int categoryId, string recipeName);
    Task<List<Recipe>> GetByTagId(int tagId);
	Task<List<Recipe>> GetByIngredientId(int ingredientId);
	Task<List<Recipe>> GetByIngredientIds(List<int> ingredientIds);
	Task<List<Recipe>> GetByUserId(string userId);
	Task<Recipe> GetByRecipeId(int recipeId);
	Task<int> Create(Recipe recipe);
	Task<bool> Update(Recipe recipe);
	Task<bool> Delete(int recipeId);
}