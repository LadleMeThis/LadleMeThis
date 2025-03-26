using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Data.Seeder;

public class DataSeeder(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    LadleMeThisContext context)
{
    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedUsersAsync();
        await SeedCategoriesAsync();
        await SeedIngredientsAsync();
        await SeedTagsAsync();
        await SeedRecipesAsync();
        await SeedRatingsAsync();
    }

    private async Task SeedRolesAsync()
    {
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        var users = new[]
        {
            new { UserName = "admin@example.com", Email = "admin@example.com", Password = "Admin@123", Role = "Admin" },
            new { UserName = "user@example.com", Email = "user@example.com", Password = "User@123", Role = "User" }
        };

        foreach (var userInfo in users)
        {
            if (await userManager.FindByEmailAsync(userInfo.Email) == null)
            {
                var user = new IdentityUser
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, userInfo.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userInfo.Role);
                }
                else
                {
                    throw new Exception($"Failed to create user {userInfo.UserName}: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }

    private async Task SeedCategoriesAsync()
    {
        if (!await context.Categories.AnyAsync())
        {
            var categories = new[]
            {
                new Category { Name = "Breakfast" },
                new Category { Name = "Dessert" },
                new Category { Name = "Main Course" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }
    }

   private async Task SeedRecipesAsync()
{
    if (!await context.Recipes.AnyAsync())
    {
        var user = await userManager.FindByEmailAsync("admin@example.com");
        if (user == null) throw new Exception("User not found");

        var categories = await context.Categories.ToListAsync();
        if (!categories.Any()) throw new Exception("Categories not found");

        var ingredients = await context.Ingredients.ToListAsync();
        if (!ingredients.Any()) throw new Exception("Ingredients not found");

        var tags = await context.Tags.ToListAsync();
        if (!tags.Any()) throw new Exception("Tags not found");

        var recipes = new[]
        {
            new Recipe
            {
                Name = "Pancakes",
                Instructions = "Mix ingredients and cook on a hot griddle.",
                PrepTime = 10,
                CookTime = 15,
                ServingSize = 4,
                User = user,
                Categories = new List<Category> { categories.FirstOrDefault(c => c.CategoryId == 1) },
                Tags = new List<Tag> { tags.FirstOrDefault(t => t.TagId == 1) },
                Ingredients = new List<Ingredient>
                {
                    ingredients.FirstOrDefault(i => i.IngredientId == 1),
                    ingredients.FirstOrDefault(i => i.IngredientId == 2),
                    ingredients.FirstOrDefault(i => i.IngredientId == 3)
                }
            },
            new Recipe
            {
                Name = "Chocolate Cake",
                Instructions = "Combine ingredients, bake at 350°F for 30 minutes.",
                PrepTime = 20,
                CookTime = 30,
                ServingSize = 8,
                User = user,
                Categories = new List<Category> { categories.FirstOrDefault(c => c.CategoryId == 1) },
                Tags = new List<Tag> { tags.FirstOrDefault(t => t.TagId == 1) },
                Ingredients = new List<Ingredient>
                {
                    ingredients.FirstOrDefault(i => i.IngredientId == 1),
                    ingredients.FirstOrDefault(i => i.IngredientId == 2),
                    ingredients.FirstOrDefault(i => i.IngredientId == 3)
                }
            }
        };

        context.Recipes.AddRange(recipes);
        await context.SaveChangesAsync();
    }
}



    private async Task SeedIngredientsAsync()
    {
        if (!await context.Ingredients.AnyAsync())
        {
            var ingredients = new[]
            {
                new Ingredient { Name = "Flour", Unit = "kg"},
                new Ingredient { Name = "Sugar", Unit = "kg" },
                new Ingredient { Name = "Eggs", Unit = "pcs" }
            };

            context.Ingredients.AddRange(ingredients);
            await context.SaveChangesAsync();
        }
    }

    private async Task SeedTagsAsync()
    {
        if (!await context.Tags.AnyAsync())
        {
            var tags = new[]
            {
                new Tag { Name = "Easy" },
                new Tag { Name = "Sweet" }
            };

            context.Tags.AddRange(tags);
            await context.SaveChangesAsync();
        }
    }

    private async Task SeedRatingsAsync()
    {
        if (!await context.Ratings.AnyAsync())
        {
            var user = await userManager.FindByEmailAsync("user@example.com");
            if (user == null) throw new Exception("User not found");
            
            var recipes = await context.Recipes.ToListAsync();
            if (!recipes.Any()) throw new Exception("Recipes not found");
            
            var ratings = new[]
            {
                new RecipeRating() { Recipe = recipes.FirstOrDefault(r => r.RecipeId == 1) ,  User = user, Rating = 5, Review = "LGTM", DateCreated = DateTime.Today},
                new RecipeRating() { Recipe = recipes.FirstOrDefault(r => r.RecipeId == 2) , User = user, Rating = 4, Review = "LGTM", DateCreated = DateTime.Today}
            };

            context.Ratings.AddRange(ratings);
            await context.SaveChangesAsync();
        }
    }
}