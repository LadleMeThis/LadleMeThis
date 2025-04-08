using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Data.Entity;

public class Recipe
{
	public int RecipeId { get; set; }
	public string Name { get; set; }
	public string Instructions { get; set; }
	public int PrepTime { get; set; }
	public int CookTime { get; set; }
	public int ServingSize { get; set; }
	public RecipeImage? RecipeImage { get; set; }
	public DateTime DateCreated { get; } = DateTime.UtcNow;
	public IdentityUser User { get; set; }
	public ICollection<Category> Categories { get; set; } = [];
	public ICollection<Tag> Tags { get; set; } = [];
	public ICollection<Ingredient> Ingredients { get; set; } = [];
	public ICollection<RecipeRating> Ratings { get; set; } = [];
}
