namespace LadleMeThis.Models.CategoryModels
{
    public class Category
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
