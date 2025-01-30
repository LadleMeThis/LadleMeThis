namespace LadleMeThis.Models.RecipeModels;

public record FullRecipeDto(
	int RecipeId,
	string Name,
	string Instructions,
	int PrepTime,
	int CookTime,
	int ServingSize,
	int UserId,
	List<CategoryDTO> Categories,
	List<TagDTO> Tags,
	List<IngredientDTO> Ingredients,
	List<RecipeRatingDTO> Ratings
);