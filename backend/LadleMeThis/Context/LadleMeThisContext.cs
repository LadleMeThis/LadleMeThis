using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Models.TagModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LadleMeThis.Context;

public class LadleMeThisContext(DbContextOptions options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
	public DbSet<Recipe> Recipes { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Ingredient> Ingredients { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<RecipeRating> Ratings { get; set; }
	public DbSet<SavedRecipe> SavedRecipes { get; set; }
	public DbSet<RecipeImage> RecipeImages { get; set; }
	

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Recipe>()
			.HasOne(r => r.User)
			.WithMany()
			.IsRequired(false);

		modelBuilder.Entity<RecipeRating>()
			.HasKey(r => r.RatingId);
		
		base.OnModelCreating(modelBuilder);
	}
}