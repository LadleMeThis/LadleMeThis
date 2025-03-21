using LadleMeThis.Models.RecipeModels;

namespace LadleMeThis.Models.CategoryModels;

public class Category
{
	public int CategoryId { get; set; }
	public string Name { get; init; }
	public ICollection<Recipe> Recipes { get; set; }
}