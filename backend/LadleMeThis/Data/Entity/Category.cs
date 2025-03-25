namespace LadleMeThis.Data.Entity;

public class Category
{
	public int CategoryId { get; set; }
	public string Name { get; init; }
	public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}