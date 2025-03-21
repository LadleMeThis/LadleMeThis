namespace LadleMeThis.Models.RecipeModels;

public record CreateRecipeDTO(
	string Name,
	int PrepTime,
	int CookTime,
	string Instructions,
	int ServingSize,
	int[] Ingredients,
	int[] Tags,
	int[] Categories
);