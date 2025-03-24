using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Data.Entity;

public class RecipeRating
{
	public int RatingId { get; set; }
	public string Review { get; set; }
	public int Rating { get; set; }
	public DateTime DateCreated { get; set; }
	public Recipe Recipe { get; set; }
	public IdentityUser User { get; set; }
}