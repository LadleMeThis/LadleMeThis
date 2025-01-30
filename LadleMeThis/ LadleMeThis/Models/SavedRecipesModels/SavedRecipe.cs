using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Models.RecipeModels
{
    public class SavedRecipe
    {
        public int UserId { get; set; }  
        public int RecipeId { get; set; }  

        public DateTime DateSaved { get; set; } = DateTime.UtcNow; 

        // Navigációk
        public User User { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
