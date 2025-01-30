using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Context;

public class LadleMeThisContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Recipe> Recipes { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Ingredient> Ingredients { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<SavedRecipe> SavedRecipes { get; set; }
}