using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Models.RecipeModels;

public record RecipeCardDTO( 
	int RecipeId,
	string Name,
	int FullTime,
	int ServingSize,
	int Rating,
	Tag[] Tags,
	Category[] Categories
	);