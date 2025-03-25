using System.ComponentModel.DataAnnotations.Schema;

namespace LadleMeThis.Models.IngredientsModels
{
    [NotMapped]
    public class IngredientDTO
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
