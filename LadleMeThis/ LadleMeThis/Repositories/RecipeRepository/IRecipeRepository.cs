using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Repositories.RecipeRepository;

public interface IRecipeRepository
{
	Task<List<Recipe>> GetAll();
	Task<List<Recipe>> GetByCategoryId(int categoryId);
	Task<List<Recipe>> GetByTagId(int tagId);
	Task<List<Recipe>> GetByIngredientId(int ingredientId);
	Task<List<Recipe>> GetByUserId(int userId);
	Task<Recipe> GetByRecipeId(int recipeId);
	Task<int> Create(Recipe recipe);
	Task<bool> Update(Recipe recipe);
	Task<bool> Delete(int recipeId);
}