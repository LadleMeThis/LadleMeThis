namespace LadleMeThis.Models.TagModels
{
    public class Tag
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
