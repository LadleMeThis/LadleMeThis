using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Models.RecipeModels;

public record FullRecipeDTO(
	int RecipeId,
	string Name,
	string Instructions,
	int PrepTime,
	int CookTime,
	int ServingSize,
	string UserName,
	string Picture,
	List<CategoryDTO> Categories,
	List<TagDTO> Tags,
	List<IngredientDTO> Ingredients,
	List<RecipeRatingDTO> Ratings
);