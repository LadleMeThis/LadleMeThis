using System.ComponentModel.DataAnnotations.Schema;

namespace LadleMeThis.Models.CategoryModels
{
    [NotMapped]
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
