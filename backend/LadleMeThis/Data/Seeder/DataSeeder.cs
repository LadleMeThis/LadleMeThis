using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using LadleMeThis.Services.FoodImageService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Data.Seeder;

public class DataSeeder(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    LadleMeThisContext context,
    IFoodImageService foodImgService)
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
        "Whisk the clouds into the golden mist until they start to hum.",
        "Gently fold the morning dew into the starlight, and let it rest.",
        "Grate the moonbeams and sprinkle them across the dream dust.",
        "Stir the ocean breeze with a spoonful of forgotten dreams.",
        "Simmer the last sunrise until the flavors of the past are awakened.",
        "Melt a handful of wishes and mix them with a dash of daylight.",
        "Boil the winds of yesterday until the aroma becomes timeless.",
        "Gather the forgotten seeds and let them sprout under the evening glow.",
        "Toss the twilight into the mix and let it mellow.",
        "Combine the scent of rain with the warmth of a memory.",
        "Knead the earth’s pulse and let the dough rise with anticipation.",
        "Toast the first star and sprinkle it over the moonlit mixture.",
        "Fold the sands of time into the batter and let it settle.",
        "Mash the echoes of the day into a smooth paste of wonder.",
        "Let the shadow of the tree grow over the bowl while it rests.",
        "Churn the mist of the forest into a frothy concoction of delight.",
        "Blend the whispers of the sea with a splash of forgotten hours.",
        "Heat the breeze until it dances in the air with fragrance.",
        "Crush the light of the full moon into tiny, shimmering pieces.",
        "Stir the fireflies until their glow turns into a subtle warmth.",
        "Combine the echoes of the forest with the song of the wind.",
        "Let the silence of the mountains pour into the pot and simmer.",
        "Mash the rays of dawn until they’re soft and spreadable.",
        "Blend the distant stars with the warmth of an old memory.",
        "Let the snowflakes melt into a creamy concoction of dreams.",
        "Swirl the morning mist into the clouds until it forms a silky texture.",
        "Roll the laughter of the past into soft, fluffy layers.",
        "Season with a pinch of forgotten time and a dash of sunrise.",
        "Whip the wind until it’s light and airy, then fold it into the mix.",
        "Slowly pour the river’s song into the bowl and stir gently.",
        "Grill the soft whispers of dawn until they’re crisp and golden.",
        "Blend the first light of day with the fading shadows of night.",
        "Press the soft petals of evening into the dough and let them bloom.",
        "Heat the twilight until it reaches the perfect balance of warmth.",
        "Mash the soft whispers of the past into a smooth paste of flavor.",
        "Combine the essence of the forest with the lightness of clouds.",
        "Stir the slow-moving river into a silky, smooth consistency.",
        "Let the night’s breeze mix with the twilight until it becomes one.",
        "Toss the golden glow of dawn into the mixture and let it settle."
    };

    private static readonly Random Random = new Random();

    private static readonly Dictionary<int, string> FoodReviews = new Dictionary<int, string>
    {
        { 1, "Oh, wow. This was... something. Definitely not sure what, but I'm sure someone out there will think it’s a ‘unique’ experience." },
        { 2, "Well, it's food. That's about all I can say. It’s not awful, but don’t get too excited – you’ll live, I guess." },
        { 3, "It's... fine. If you're the kind of person who settles for 'okay,' then this is your dream meal. Nothing more, nothing less." },
        { 4, "Now this one’s a bit of a shocker! Actually, not terrible. I might even come back for seconds... if I’m really hungry and have nothing else to do." },
        { 5, "Oh look, it’s the golden standard of meals. Perfect. Like, if I could eat this for every meal without question, I totally would... but only because I don’t have better options." }
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
        await SeedPicturesAsync();
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
                new Ingredient { Name = "Dreamwhispers", Unit = "g" },
                new Ingredient { Name = "Hopeberries", Unit = "kg" },
                new Ingredient { Name = "Despairdust", Unit = "ml" },
                new Ingredient { Name = "Wings of Wonder", Unit = "pcs" },
                new Ingredient { Name = "Glimmering Regret", Unit = "g" },
                new Ingredient { Name = "Mourning Dew", Unit = "L" },
                new Ingredient { Name = "Fearseed", Unit = "ml" },
                new Ingredient { Name = "Joycrystals", Unit = "kg" },
                new Ingredient { Name = "Echo of the Past", Unit = "ml" },
                new Ingredient { Name = "Sorrowflame", Unit = "g" },
                new Ingredient { Name = "Whispering Shadows", Unit = "pcs" },
                new Ingredient { Name = "Laughterroot", Unit = "L" },
                new Ingredient { Name = "Moonlit Frustration", Unit = "g" },
                new Ingredient { Name = "Angerpetals", Unit = "kg" },
                new Ingredient { Name = "Mirthsugar", Unit = "g" },
                new Ingredient { Name = "Loneliness Essence", Unit = "ml" },
                new Ingredient { Name = "Exhilaration Spritz", Unit = "g" },
                new Ingredient { Name = "Bitter Hope", Unit = "kg" },
                new Ingredient { Name = "Furyblossom", Unit = "pcs" },
                new Ingredient { Name = "Sunshine Sighs", Unit = "g" },
                new Ingredient { Name = "Dreamsnap", Unit = "L" },
                new Ingredient { Name = "Tearflour", Unit = "g" },
                new Ingredient { Name = "Whimsyroot", Unit = "ml" },
                new Ingredient { Name = "Frostbitten Smiles", Unit = "kg" },
                new Ingredient { Name = "Hopeful Sparks", Unit = "g" },
                new Ingredient { Name = "Silent Lullaby", Unit = "g" },
                new Ingredient { Name = "Crying Petal", Unit = "kg" },
                new Ingredient { Name = "Dreadwhirl", Unit = "pcs" },
                new Ingredient { Name = "Desire Vines", Unit = "g" },
                new Ingredient { Name = "Wistful Sorrows", Unit = "L" },
                new Ingredient { Name = "Tearstalks", Unit = "ml" },
                new Ingredient { Name = "Blissfog", Unit = "g" },
                new Ingredient { Name = "Longing Fumes", Unit = "kg" },
                new Ingredient { Name = "Shattered Hopes", Unit = "pcs" }
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
                new Tag { Name = "Easy Breezy" },
                new Tag { Name = "Savory Dreams" },
                new Tag { Name = "Heartwarming" },
                new Tag { Name = "Vegan Vibes" },
                new Tag { Name = "Lactose No-More" },
                new Tag { Name = "Spicy Whispers" },
                new Tag { Name = "Comfort in a Bowl" },
                new Tag { Name = "Mouthful of Joy" },
                new Tag { Name = "Zesty Surprises" },
                new Tag { Name = "Midnight Snack" },
                new Tag { Name = "Mellow Magic" },
                new Tag { Name = "Crunchy Cravings" },
                new Tag { Name = "Sweet Escape" },
                new Tag { Name = "Nourishing Nirvana" },
                new Tag { Name = "Cheat Day Treat" },
                new Tag { Name = "Heatwave" }
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

    private async Task SeedPicturesAsync()
    {
        var recipes = await context.Recipes.ToListAsync();
        if (!recipes.Any()) throw new Exception("Recipes not found");

        foreach (var recipe in recipes)
        {
            if (recipe.RecipePicture == null)
            {
                recipe.RecipePicture = await foodImgService.GetRandomFoodImageUrlAsync();
            }
        }

    }

}