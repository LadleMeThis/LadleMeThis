namespace LadleMeThis.Data.Entity;

public class Tag
{
	public int TagId { get; set; }
	public string Name { get; init; }
	public ICollection<Recipe> Recipes { get; set; }
}