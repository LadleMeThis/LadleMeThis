using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.SavedRecipesModels;

namespace LadleMeThis.Models.UserModels;

public class User
{
		public int UserId { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string DisplayName { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		
		public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
		public ICollection<SavedRecipe> SavedRecipes { get; set; } = new List<SavedRecipe>();
}