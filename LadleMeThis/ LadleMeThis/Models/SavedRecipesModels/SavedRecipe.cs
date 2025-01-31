using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Models.SavedRecipesModels;

public class SavedRecipe
{
	public int UserId { get; set; }  
	public int RecipeId { get; set; }  

	public DateTime DateSaved { get; set; } = DateTime.UtcNow; 

	public User User { get; set; } = null!;
	public Recipe Recipe { get; set; } = null!;
}