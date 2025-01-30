using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Models.RecipeRatings;

public class RecipeRating
{
	public int RatingId { get; set; }
	public string Review { get; set; }
	public int Rating { get; set; }
	public DateTime DateCreated { get; set; }
	
	public int RecipeId { get; set; }
	public Recipe Recipe { get; set; }
	
	public int UserId { get; set; }
	public User User { get; set; }
}