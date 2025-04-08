using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Data.Entity
{
    public class RecipeImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string PhotographerName { get; set; }
        public string PhotographerUrl {  get; set; }
        public string Alt {  get; set; }
    }
}
