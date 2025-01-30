namespace LadleMeThis.Models.IngredientsModels
{
    public class Ingredient
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Unit { get; init; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}