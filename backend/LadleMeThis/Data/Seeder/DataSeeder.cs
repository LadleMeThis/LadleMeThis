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
	private static readonly string[] RecipeNames = new[]
	{
		"Fluffernut Pie",
		"Crimson Crumble",
		"Zesty Torte",
		"Golden Tacos",
		"Velvet Pudding",
		"Saffron Whisk",
		"Crunchy Delight",
		"Whispering Stew",
		"Breezy Bites",
		"Moonlit Mash",
		"Frosted Frittata",
		"Honeycomb Surprise",
		"Twilight Toast",
		"Crisp Haven",
		"Dusky Dumplings",
		"Sunset Ragu",
		"Sugarplum Stew",
		"Cinnamon Spire",
		"Lush Loaf",
		"Wildberry Glaze",
		"Softwave Souffle",
		"Lime Drizzle",
		"Frosted Fables",
		"Amber Snap",
		"Peach Bloom",
		"Mellow Quiche",
		"Caramel Crest",
		"Pineapple Halo",
		"Butterleaf Bake",
		"Golden Root Bake",
		"Mango Mirth",
		"Twilight Tart",
		"Cocoa Frost",
		"Dreamy Dumplings",
		"Mystic Soup",
		"ChocoWhirl Bites",
		"Silk Whisk",
		"Emerald Casserole",
		"Crisp Mirage",
		"Honey Dew Delight",
		"Lemon Quake",
		"Lavender Crisp",
		"Frothy Quinoa",
		"Sunset Zing",
		"Silk Sizzle",
		"Golden Glimmer Bites",
		"Blueberry Whisk",
		"Spicy Moon",
		"Ginger Swirl",
		"Fizzy Feast",
		"Berry Nova",
		"Honey Smash",
		"Citrus Breeze",
		"Cloud Pie",
		"Mystic Tacos",
		"Tropical Dream",
		"Savory Bloom",
		"Snowflake Stew",
		"Breeze Pudding",
		"Sizzling Splice",
		"Vivid Ribs",
		"Cinnamon Glow",
		"Sunset Drift",
		"Honeyfrost Twist",
		"Sugarwhirl Stew",
		"Tangerine Spin",
		"Shimmer Spire",
		"Crimson Lace",
		"Frozen Ember",
		"Coconut Drizzle",
		"Plum Kiss",
		"Berry Whisper",
		"Ginger Honeycomb",
		"Golden Bloom",
		"Spicy Mirage",
		"Moonbeam Casserole",
		"Ivory Drizzle",
		"Warm Velvet",
		"Frosted Delight",
		"Tart Gem",
		"Sugar Surge",
		"Citrus Bliss",
		"Toffee Swirl",
		"Maple Glow",
		"Crisp Jewel",
		"Vanilla Whisk",
		"Sparkle Bake",
		"Autumn Frost",
		"Crisp Twist",
		"Lush Whisk",
		"Lemon Mist",
		"Ginger Splash",
		"Spiced Crust",
		"Berry Glow",
		"Caramel Moon",
		"Fluffy Nectar",
		"Crispy Gem",
		"Twilight Glaze"
	};

	private static readonly string[] Instructions = new[]
	{
		"Fluffernut Pie",
		"Crimson Crumble",
		"Zesty Torte",
		"Golden Tacos",
		"Velvet Pudding",
		"Saffron Whisk",
		"Crunchy Delight",
		"Whispering Stew",
		"Breezy Bites",
		"Moonlit Mash",
		"Frosted Frittata",
		"Honeycomb Surprise",
		"Twilight Toast",
		"Crisp Haven",
		"Dusky Dumplings",
		"Sunset Ragu",
		"Sugarplum Stew",
		"Cinnamon Spire",
		"Lush Loaf",
		"Wildberry Glaze",
		"Softwave Souffle",
		"Lime Drizzle",
		"Frosted Fables",
		"Amber Snap",
		"Peach Bloom",
		"Mellow Quiche",
		"Caramel Crest",
		"Pineapple Halo",
		"Butterleaf Bake",
		"Golden Root Bake",
		"Mango Mirth",
		"Twilight Tart",
		"Cocoa Frost",
		"Dreamy Dumplings",
		"Mystic Soup",
		"ChocoWhirl Bites",
		"Silk Whisk",
		"Emerald Casserole",
		"Crisp Mirage",
		"Honey Dew Delight",
		"Lemon Quake",
		"Lavender Crisp",
		"Frothy Quinoa",
		"Sunset Zing",
		"Silk Sizzle",
		"Golden Glimmer Bites",
		"Blueberry Whisk",
		"Spicy Moon",
		"Ginger Swirl",
		"Fizzy Feast",
		"Berry Nova",
		"Honey Smash",
		"Citrus Breeze",
		"Cloud Pie",
		"Mystic Tacos",
		"Tropical Dream",
		"Savory Bloom",
		"Snowflake Stew",
		"Breeze Pudding",
		"Sizzling Splice",
		"Vivid Ribs",
		"Cinnamon Glow",
		"Sunset Drift",
		"Honeyfrost Twist",
		"Sugarwhirl Stew",
		"Tangerine Spin",
		"Shimmer Spire",
		"Crimson Lace",
		"Frozen Ember",
		"Coconut Drizzle",
		"Plum Kiss",
		"Berry Whisper",
		"Ginger Honeycomb",
		"Golden Bloom",
		"Spicy Mirage",
		"Moonbeam Casserole",
		"Ivory Drizzle",
		"Warm Velvet",
		"Frosted Delight",
		"Tart Gem",
		"Sugar Surge",
		"Citrus Bliss",
		"Toffee Swirl",
		"Maple Glow",
		"Crisp Jewel",
		"Vanilla Whisk",
		"Sparkle Bake",
		"Autumn Frost",
		"Crisp Twist",
		"Lush Whisk",
		"Lemon Mist",
		"Ginger Splash",
		"Spiced Crust",
		"Berry Glow",
		"Caramel Moon",
		"Fluffy Nectar",
		"Crispy Gem",
		"Twilight Glaze"
	};

	private static readonly Random Random = new Random();

	private static readonly Dictionary<int, string> FoodReviews = new Dictionary<int, string>
	{
		{ 1, "Not quite there, left me wanting more. Might work for someone else, but not for me." },
		{ 2, "Somewhat edible, but it's missing something. It's okay, but I wouldn't rush back." },
		{ 3, "Decent enough. Nothing extraordinary, but it gets the job done. Passable at best." },
		{ 4, "Quite good! A solid option with a few surprises. Definitely worth trying again." },
		{ 5, "LGTM – Absolutely perfect. Everything you need, nothing you don’t. Highly recommend!" }
	};


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
					throw new Exception(
						$"Failed to create user {userInfo.UserName}: {string.Join(", ", result.Errors)}");
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
				new Category { Name = "Morning Delights" },
				new Category { Name = "Sweet Indulgences" },
				new Category { Name = "Hearty Creations" },
				new Category { Name = "Tiny Bites" },
				new Category { Name = "Green Offerings" },
				new Category { Name = "Quick Nibbles" },
				new Category { Name = "Liquid Refreshment" }
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

			var recipes = RecipeNames.Select(recipeName =>
			{
				var numInstructions = Random.Next(1, 8);
				var randomInstructions = Instructions.OrderBy(x => Guid.NewGuid()).Take(numInstructions).ToList();
				var instructionsText = string.Join(" ", randomInstructions);
				var categoryList = categories.OrderBy(c => Guid.NewGuid())
					.Take(Random.Next(1, 4))
					.ToList();
				var tagList = tags.OrderBy(t => Guid.NewGuid())
					.Take(Random.Next(2, 6))
					.ToList();
				var ingredientList = ingredients.OrderBy(i => Guid.NewGuid())
					.Take(Random.Next(3, 13))
					.ToList();

				return new Recipe
				{
					Name = recipeName,
					Instructions = instructionsText,
					PrepTime = Random.Next(5, 31),
					CookTime = Random.Next(10, 61),
					ServingSize = Random.Next(1, 11),
					User = user,
					Categories = categoryList,
					Tags = tagList,
					Ingredients = ingredientList
				};
			}).ToList();

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
				new Ingredient { Name = "Flour", Unit = "kg" },
				new Ingredient { Name = "Sugar", Unit = "kg" },
				new Ingredient { Name = "Eggs", Unit = "pcs" },
				new Ingredient { Name = "Butter", Unit = "kg" },
				new Ingredient { Name = "Salt", Unit = "g" },
				new Ingredient { Name = "Milk", Unit = "L" },
				new Ingredient { Name = "Vanilla Extract", Unit = "ml" },
				new Ingredient { Name = "Baking Powder", Unit = "g" },
				new Ingredient { Name = "Cocoa Powder", Unit = "g" },
				new Ingredient { Name = "Lemon Juice", Unit = "ml" },
				new Ingredient { Name = "Olive Oil", Unit = "L" },
				new Ingredient { Name = "Garlic", Unit = "pcs" },
				new Ingredient { Name = "Tomato", Unit = "pcs" },
				new Ingredient { Name = "Onion", Unit = "pcs" },
				new Ingredient { Name = "Cheese", Unit = "kg" },
				new Ingredient { Name = "Chicken Breast", Unit = "kg" },
				new Ingredient { Name = "Ground Beef", Unit = "kg" },
				new Ingredient { Name = "Rice", Unit = "kg" },
				new Ingredient { Name = "Cinnamon", Unit = "g" },
				new Ingredient { Name = "Basil", Unit = "g" },
				new Ingredient { Name = "Chili Pepper", Unit = "g" },
				new Ingredient { Name = "Yogurt", Unit = "L" },
				new Ingredient { Name = "Honey", Unit = "g" },
				new Ingredient { Name = "Pistachio", Unit = "g" }
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
				new Tag { Name = "Sweet" },
				new Tag { Name = "Savory" },
				new Tag { Name = "Quick" },
				new Tag { Name = "Healthy" },
				new Tag { Name = "Vegan" },
				new Tag { Name = "Lactose-Free" },
				new Tag { Name = "Spicy" },
				new Tag { Name = "Comfort Food" }
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

			var ratings = new List<RecipeRating>();

			foreach (var recipe in recipes)
			{
				var ratingCount = Random.Next(1, 6);

				var randomRatings = Enumerable.Range(0, ratingCount)
					.Select(_ => Random.Next(1, 6))
					.ToList();

				var randomDates = Enumerable.Range(0, ratingCount)
					.Select(_ => DateTime.Today.AddDays(Random.Next(-30, 1)))
					.ToList();


				for (var i = 0; i < ratingCount; i++)
				{
					var rating = randomRatings[i];
					var review = FoodReviews[rating];
					var randomDate = randomDates[i];

					ratings.Add(new RecipeRating
					{
						Recipe = recipe,
						User = user,
						Rating = rating,
						Review = review,
						DateCreated = randomDate
					});
				}
			}

			context.Ratings.AddRange(ratings);
			await context.SaveChangesAsync();
		}
	}
}