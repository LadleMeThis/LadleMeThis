using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Data.Entity;

public class SavedRecipe
{
	public DateTime DateSaved { get; set; } = DateTime.UtcNow; 

	public string UserId { get; set; }
	public IdentityUser User { get; set; } = null!;
	public int RecipeId { get; set; }
	public Recipe Recipe { get; set; } = null!;
}