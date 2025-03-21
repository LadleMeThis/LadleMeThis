using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Data.Entity;

public class SavedRecipe
{
	public DateTime DateSaved { get; set; } = DateTime.UtcNow; 

	public IdentityUser User { get; set; } = null!;
	public Recipe Recipe { get; set; } = null!;
}