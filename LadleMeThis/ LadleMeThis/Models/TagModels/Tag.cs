using LadleMeThis.Models.RecipeModels;

namespace LadleMeThis.Models.TagModels;

public class Tag
{
	public int TagId { get; set; }
	public string Name { get; init; }
	public ICollection<Recipe> Recipes { get; set; }
}