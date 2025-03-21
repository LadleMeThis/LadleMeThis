using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Services.RecipeDetailService;

public interface IRecipeDetailService
{
	Task<IEnumerable<Tag>> GetTagsByIds(int[] tagIds);
	Task<IEnumerable<Category>> GetCategoriesByIds(int[] categoryIds);
	Task<IEnumerable<Ingredient>> GetIngredientsByIds(int[] ingredientIds);
	IEnumerable<TagDTO> GetTagDTOsByTags(IEnumerable<Tag> tags);
	IEnumerable<IngredientDTO> GetIngredientDTOsByIngredients(IEnumerable<Ingredient> ingredients); 
	IEnumerable<CategoryDTO> GetCategoryDTOsByCategories(IEnumerable<Category> categories);
}