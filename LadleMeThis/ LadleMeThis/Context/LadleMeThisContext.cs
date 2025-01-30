using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatings;
using LadleMeThis.Models.SavedRecipesModels;;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LadleMeThis.Context;

public class LadleMeThisContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Recipe> Recipes { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Ingredient> Ingredients { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<RecipeRating> Ratings { get; set; }
	public DbSet<SavedRecipe> SavedRecipes { get; set; }
}