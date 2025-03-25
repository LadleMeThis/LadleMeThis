namespace LadleMeThis.Data.Entity;

public class Ingredient
{
	public int IngredientId { get; set; }
	public string Name { get; init; }
	public string Unit { get; init; }
	public ICollection<Recipe> Recipes { get; set; }
}