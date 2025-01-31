using System.ComponentModel.DataAnnotations.Schema;

namespace LadleMeThis.Models.TagModels
{
    [NotMapped]
    public class TagDTO
    {
        public int TagId { get; set; }
        public string Name { get; set; }
    }
}
