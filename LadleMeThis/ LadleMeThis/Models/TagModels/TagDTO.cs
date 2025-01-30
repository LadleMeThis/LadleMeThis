using System.ComponentModel.DataAnnotations.Schema;

namespace LadleMeThis.Models.TagModels
{
    [NotMapped]
    public class TagDTO
    {
        public string Name { get; set; }
    }
}
