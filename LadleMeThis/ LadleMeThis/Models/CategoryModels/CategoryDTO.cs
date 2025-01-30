using System.ComponentModel.DataAnnotations.Schema;

namespace LadleMeThis.Models.CategoryModels
{
    [NotMapped]
    public class CategoryDTO
    {
        public string Name { get; set; }
    }
}
