using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LadleMethisIntegrationTests.Seeder;

public class TestDataSeeder(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    LadleMeThisContext context)
{
    private static readonly Random Random = new Random();
    private static readonly string Role = "User";

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
        if (!await roleManager.RoleExistsAsync(Role))
            await roleManager.CreateAsync(new IdentityRole(Role));
    }

    private async Task SeedUsersAsync()
    {
        var password = "Test@123";

        var testUser = new IdentityUser
        {
            UserName = "test_example",
            Email = "test@example.com",
            EmailConfirmed = true
        };

        var testUserWithoutRecipe = new IdentityUser
        {
            UserName = "testUserWithoutRecipe",
            Email = "testUserWithoutRecipe@example.com",
            EmailConfirmed = true
        };

        var testUsers = new List<IdentityUser>
        {
            testUser,
            testUserWithoutRecipe,
        };

        foreach (var user in testUsers)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role);
                }
                else
                {
                    throw new Exception(
                        $"Failed to create user {user.UserName}: {string.Join(", ", result.Errors)}");
                }
            }

        }

    }


    private async Task SeedCategoriesAsync()
    {
        if (!await context.Categories.AnyAsync())
        {
            var category0 = new Category { Name = "Test Category1" };
            var category1 = new Category { Name = "Test Category2" };
            var category2 = new Category { Name = "Test Category3" };

            var categories = new List<Category> { category0, category1, category2 };


            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            await context.SaveChangesAsync();
        }
    }

    private async Task SeedRecipesAsync()
    {
        if (!await context.Recipes.AnyAsync())
        {
            var user = await userManager.FindByEmailAsync("test@example.com");
            if (user == null)
                throw new Exception("User not found");

            var categories = await context.Categories.ToListAsync();
            if (!categories.Any())
                throw new Exception("Categories not found");

            var ingredients = await context.Ingredients.ToListAsync();
            if (!ingredients.Any())
                throw new Exception("Ingredients not found");

            var tags = await context.Tags.ToListAsync();
            if (!tags.Any())
                throw new Exception("Tags not found");

            var categoryList = categories.OrderBy(c => Guid.NewGuid())
                .ToList();
            var tagList = tags.OrderBy(t => Guid.NewGuid())
                .ToList();
            var ingredientList = ingredients.OrderBy(i => Guid.NewGuid())
                .ToList();

            const string recipeName = "Test Recipe";
            const string instructions = "Test instructions";


            var recipe = new Recipe
            {
                Name = recipeName,
                Instructions = instructions,
                PrepTime = Random.Next(5, 31),
                CookTime = Random.Next(10, 61),
                ServingSize = Random.Next(1, 11),
                User = user,
                Categories = categoryList,
                Tags = tagList,
                Ingredients = ingredientList,
                RecipeImage = new RecipeImage()
                {
                    ImageId = 1,
                    ImageUrl = "TestImageUrl",
                    PhotographerName = "TestPhotographerName",
                    PhotographerUrl = "TestPhotographerUrl",
                    Alt = "TestAlt"
                }
            };

            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
        }
    }


    private async Task SeedIngredientsAsync()
    {
        if (!await context.Ingredients.AnyAsync())
        {
            var ingredients = new[]
            {
                new Ingredient { Name = "Test ingredient 1", Unit = "g" },
                new Ingredient { Name = "Test ingredient 2", Unit = "g" },
                new Ingredient { Name = "Test ingredient 3", Unit = "g" },
                new Ingredient { Name = "Test ingredient 4", Unit = "g" },
                new Ingredient { Name = "Test ingredient 5", Unit = "g" },
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
                new Tag { Name = "Test tag 1" },
                new Tag { Name = "Test tag 2" },
                new Tag { Name = "Test tag 3" },
            };

            context.Tags.AddRange(tags);
            await context.SaveChangesAsync();
        }
    }

    private async Task SeedRatingsAsync()
    {
        if (!await context.Ratings.AnyAsync())
        {
            var user = await userManager.FindByEmailAsync("test@example.com");
            if (user == null) throw new Exception("User not found");

            var recipes = await context.Recipes.ToListAsync();
            if (!recipes.Any()) throw new Exception("Recipes not found");

            var ratings = new List<RecipeRating>();

            const int rating = 5;
            const string review = "Test Rating";
            var randomDate = DateTime.Today;

            foreach (var recipe in recipes)
            {
                ratings.Add(new RecipeRating
                {
                    Recipe = recipe,
                    User = user,
                    Rating = rating,
                    Review = review,
                    DateCreated = randomDate
                });
            }

            context.Ratings.AddRange(ratings);
            await context.SaveChangesAsync();
        }
    }
}