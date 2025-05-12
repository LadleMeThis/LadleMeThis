using LadleMeThisIntegrationTests;
using System.Net.Http.Json;
using System.Net;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LadleMeThis.Models.RecipeRatingsModels;

namespace LadleMethisIntegrationTests
{
    [Collection("Recipe")]
    public class RecipeControllerTests : IClassFixture<LadleMeThisFactory>
    {
        private readonly HttpClient _client;
        private readonly UserLogger _userLogger;
        private readonly LadleMeThisFactory _factory;
        private const string VALID_EMAIL = "test@example.com";
        private const string VALID_PASSWORD = "Test@123";
        private const string USER_WITHOUT_RECIPE = "testUserWithoutRecipe@example.com";



        public RecipeControllerTests(LadleMeThisFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                HandleCookies = true
            });
            _userLogger = new UserLogger(_client);
            _factory = factory;

        }

        [Fact]
        public async Task GetAllRecipes_ShouldReturnRecipesList()
        {
            // Act
            var response = await _client.GetAsync("/recipes");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();
            Assert.NotNull(recipes);
        }

        [Fact]
        public async Task GetRecipesByCategoryId_ShouldReturnRecipesWithGivenCategoryId_WhenIdIsValid()
        {
            int categoryId = 1;
            var response = await _client.GetAsync($"/recipes/category/{categoryId}");

            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();
            Assert.NotNull(recipes);
            Assert.All(recipes, recipe =>
                Assert.True(recipe.Categories.Any(c => c.CategoryId == categoryId),
                    $"Recipe with id: {recipe.RecipeId} does not contain CategoryId of: {categoryId}"));


            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

                var expectedRecipes = await context.Recipes
                    .Where(r => r.Categories.Any(c => c.CategoryId == categoryId))
                    .ToListAsync();

                Assert.Equal(expectedRecipes.Count, recipes.Count);
            }

        }

        [Fact]
        public async Task GetRecipesByCategoryId_ShouldReturnEmptyList_WhenIdIsNonExistent()
        {
            int categoryId = -1;
            var response = await _client.GetAsync($"/recipes/category/{categoryId}");
            response.EnsureSuccessStatusCode();

            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.Empty(recipes);

        }

        [Fact]
        public async Task CreateRecipe_LoggedInUser_ShouldReturnOkAndRecipeId()
        {
            const string recipeName = "Delicious Recipe";
            // Arrange
            var createRecipeDto = new CreateRecipeDTO(
                Name: recipeName,
                PrepTime: 15,
                CookTime: 30,
                Instructions: "Mix all ingredients and cook for 30 minutes.",
                ServingSize: 4,
                Ingredients: new int[] { 1, 2 },
                Tags: new int[] { 1, 2 },
                Categories: new int[] { 1, 3 }
            );

            await _userLogger.LoginUser(VALID_EMAIL, VALID_PASSWORD);

            var response = await _client.PostAsJsonAsync("/recipes", createRecipeDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var recipeId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(recipeId > 0);
        }


        [Fact]
        public async Task CreateRecipe_NotLoggedInUser_ShouldReturnUnauthorized()
        {
            // Arrange
            var createRecipeDto = new CreateRecipeDTO(
                Name: "Delicious Recipe",
                PrepTime: 15,
                CookTime: 30,
                Instructions: "Mix all ingredients and cook for 30 minutes.",
                ServingSize: 4,
                Ingredients: new int[] { 1, 2 },
                Tags: new int[] { 1, 2 },
                Categories: new int[] { 1, 3 }
            );

            // Act
            var response = await _client.PostAsJsonAsync("/recipes", createRecipeDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetLoggedInUserRecipes_LoggedInUser_ShouldReturnRecipesIfUserHasAny()
        {
            // Arrange
            await _userLogger.LoginUser(VALID_EMAIL, VALID_PASSWORD);

            // Act
            var response = await _client.GetAsync("/recipes/my-recipes");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);
        }

        [Fact]
        public async Task GetLoggedInUserRecipes_LoggedInUser_ShouldReturnEmptyListIfUserHasNone()
        {
            // Arrange
            await _userLogger.LoginUser(USER_WITHOUT_RECIPE, VALID_PASSWORD);

            // Act
            var response = await _client.GetAsync("/recipes/my-recipes");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.Empty(recipes);
        }


        [Fact]
        public async Task GetLoggedInUserRecipes_NotLoggedInUser_ShouldReturnUnauthorized()
        {
            // Act
            var response = await _client.GetAsync("/recipes/my-recipes");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task GetRecipesByName_ValidRecipeName_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var recipeName = "Test Recipe";

            // Act
            var response = await _client.GetAsync($"/recipes/{recipeName}");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);

            Assert.True(recipes.All(r => r.Name.Contains(recipeName)));
        }

        [Fact]
        public async Task GetRecipesByName_InvalidRecipeName_ShouldReturnEmptyRecipeList()
        {
            // Arrange
            var invalidRecipeName = -1;

            // Act
            var response = await _client.GetAsync($"/recipes/{invalidRecipeName}");
            response.EnsureSuccessStatusCode();

            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();


            // Assert
            Assert.NotNull(recipes);
            Assert.Empty(recipes);
        }


        [Fact]
        public async Task GetRecipesByTagId_ValidTagId_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var validTagId = 1;

            // Act
            var response = await _client.GetAsync($"/recipes/tag/{validTagId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);


            Assert.All(recipes, recipe =>
                Assert.True(recipe.Tags.Any(t => t.TagId == validTagId),
                    $"Recipe with id: {recipe.RecipeId} does not contain TagId of: {validTagId}"));


            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

                var expectedRecipes = await context.Recipes
                    .Where(r => r.Tags.Any(t => t.TagId == validTagId))
                    .ToListAsync();

                Assert.Equal(expectedRecipes.Count, recipes.Count);
            }
        }


        [Fact]
        public async Task GetRecipesByTagId_InvalidTagId_ShouldReturnEmptyList()
        {
            // Arrange
            var invalidTagId = -1;

            // Act
            var response = await _client.GetAsync($"/recipes/tag/{invalidTagId}");

            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            // Assert
            Assert.NotNull(recipes);
            Assert.Empty(recipes);
        }


        [Fact]
        public async Task GetRecipesByIngredientId_ValidIngredientId_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var validIngredientId = 1;

            // Act
            var response = await _client.GetAsync($"/recipes/ingredient/{validIngredientId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);

            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

                var expectedRecipes = await context.Recipes
                    .Where(r => r.Ingredients.Any(i => i.IngredientId == validIngredientId))
                    .ToListAsync();

                Assert.Equal(expectedRecipes.Count, recipes.Count);
            }
        }


        [Fact]
        public async Task GetRecipesByIngredientId_InvalidIngredientId_ShouldReturnEmptyList()
        {
            // Arrange
            var invalidIngredientId = -1;

            // Act
            var response = await _client.GetAsync($"/recipes/ingredient/{invalidIngredientId}");
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            // Assert
            Assert.NotNull(recipes);
            Assert.Empty(recipes);
        }



        [Fact]
        public async Task GetRecipesByIngredientIds_ValidIds_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var ingredientIds = new List<int> { 1, 2 };
            var query = string.Join("&ingredientIds=", ingredientIds);

            // Act
            var response = await _client.GetAsync($"/recipes/ingredients?ingredientIds={query}");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);

            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

                var expectedRecipes = await context.Recipes
                    .Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.IngredientId)))
                    .ToListAsync();

                Assert.Equal(expectedRecipes.Count, recipes.Count);
            }
        }


        [Fact]
        public async Task GetRecipesByIngredientIds_InvalidIds_ShouldReturnEmptyList()
        {
            // Arrange
            var invalidIds = new List<int> { -1, -2 };
            var query = string.Join("&ingredientIds=", invalidIds);

            // Act
            var response = await _client.GetAsync($"/recipes/ingredients?ingredientIds={query}");
            response.EnsureSuccessStatusCode();
            var recipes = await response.Content.ReadFromJsonAsync<List<RecipeCardDTO>>();

            // Assert
            Assert.NotNull(recipes);
            Assert.Empty(recipes);
        }



        [Fact]
        public async Task GetRecipesByIngredientIds_WithoitIds_ShouldReturnBadRequest()
        {
            // Act
            var response = await _client.GetAsync("/recipes/ingredients");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var error = await response.Content.ReadAsStringAsync();
            Assert.Contains("Ingredient IDs must be provided", error);
        }


        [Fact]
        public async Task GetRecipeById_ValidId_ShouldReturnFullRecipe()
        {
            // Arrange
            var validRecipeId = 1;

            // Act
            var response = await _client.GetAsync($"/recipe/{validRecipeId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var recipe = await response.Content.ReadFromJsonAsync<FullRecipeDTO>();

            Assert.NotNull(recipe);
            Assert.Equal(validRecipeId, recipe.RecipeId);
        }


        [Fact]
        public async Task GetRecipeById_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var invalidRecipeId = -1;

            // Act
            var response = await _client.GetAsync($"/recipe/{invalidRecipeId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        // if you try to update the recipe with a non existent category, tag or ingredient id, it will just not do it, and it wont even inform you!

        [Fact]
        public async Task UpdateRecipe_ValidRequest_ShouldReturnNoContent()
        {
            // Arrange
            int recipeId = 1;
            string? name = "new name";
            int? prepTime = 2;
            int? cookTime = 2;
            string? instructions = "new instructions";
            int? servingSize = 3;
            int[]? ingredients = { 1, 2, 3 };
            int[]? tags = { 1, 2, 3 };
            int[]? categories = { 1, 2, 3 };

            var updateDto = new UpdateRecipeDTO(recipeId, name, prepTime, cookTime, instructions, servingSize, ingredients, tags, categories);

            var content = JsonContent.Create(updateDto);

            // Act
            var response = await _client.PutAsync($"/recipe/{recipeId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

                var updatedRecipe = context.Recipes
                    .Include(r => r.Ingredients)
                    .Include(r => r.Tags)
                    .Include(r => r.Categories)
                    .FirstOrDefault(r => r.RecipeId == recipeId);

                Assert.NotNull(updatedRecipe);

                // Ingredients
                Assert.Equal(3, updatedRecipe.Ingredients.Count);
                Assert.All(ingredients, id =>
                    Assert.Contains(updatedRecipe.Ingredients, i => i.IngredientId == id));

                // Tags
                Assert.Equal(3, updatedRecipe.Tags.Count);
                Assert.All(tags, id =>
                    Assert.Contains(updatedRecipe.Tags, t => t.TagId == id));

                // Categories
                Assert.Equal(3, updatedRecipe.Categories.Count);
                Assert.All(categories, id =>
                    Assert.Contains(updatedRecipe.Categories, c => c.CategoryId == id));

            }
        }



        [Fact]
        public async Task UpdateRecipe_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var nonExistentId = -1;
            string? name = "new name";
            int? prepTime = 2;
            int? cookTime = 2;
            string? instructions = "new instructions";
            int? servingSize = 3;
            int[]? ingredients = { 1, 2, 3 };
            int[]? tags = { 1, 2, 3 };
            int[]? categories = { 1, 2, 3 };

            var updateDto = new UpdateRecipeDTO(nonExistentId, name, prepTime, cookTime, instructions, servingSize, ingredients, tags, categories);

            var content = JsonContent.Create(updateDto);

            // Act
            var response = await _client.PutAsync($"/recipe/{nonExistentId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task UpdateRecipe_RecipeIdInUrlAndBodyDiffer_ShouldReturnBadRequest()
        {
            // Arrange
            var recipeId1 = -1;
            var recipeId2 = -2;
            string? name = "new name";
            int? prepTime = 2;
            int? cookTime = 2;
            string? instructions = "new instructions";
            int? servingSize = 3;
            int[]? ingredients = { 1, 2, 3 };
            int[]? tags = { 1, 2, 3 };
            int[]? categories = { 1, 2, 3 };

            var updateDto = new UpdateRecipeDTO(recipeId1, name, prepTime, cookTime, instructions, servingSize, ingredients, tags, categories);

            var content = JsonContent.Create(updateDto);

            // Act
            var response = await _client.PutAsync($"/recipe/{recipeId2}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var error = await response.Content.ReadAsStringAsync();
            Assert.Contains("Recipe ID mismatch", error);
        }


        [Fact]
        public async Task DeleteRecipe_ValidId_ShouldRemoveRecipe()
        {
            // Arrange
            int recipeId = 2;

            // Act
            var response = await _client.DeleteAsync($"/recipe/{recipeId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();
                var recipe = await context.Recipes.FindAsync(recipeId);
                Assert.Null(recipe);
            }
        }

        [Fact]
        public async Task DeleteRecipe_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int nonExistentId = -1;

            // Act
            var response = await _client.DeleteAsync($"/recipe/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task CreateRecipeRating_ValidId_ShouldReturnRecipeRatingId()
        {
            // Arrange
            int recipeId = 1;
            var recipeRating = new CreateRecipeRatingDTO(2, "test review");

            await _userLogger.LoginUser(VALID_EMAIL, VALID_PASSWORD);

            // Act
            var response = await _client.PostAsJsonAsync($"/recipe/{recipeId}/rating", recipeRating);


            // Assert
            response.EnsureSuccessStatusCode();

            var reviewId = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(reviewId > 0);

            using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();
                var ratings = context.Ratings.Where(r => r.Recipe.RecipeId == recipeId && r.Review == "test review");

                Assert.Single(ratings);
            }
        }


        [Fact]
        public async Task CreateRecipeRating_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int recipeId = -1;
            var recipeRating = new CreateRecipeRatingDTO(2, "test review");

            await _userLogger.LoginUser(VALID_EMAIL, VALID_PASSWORD);

            // Act
            var response = await _client.PostAsJsonAsync($"/recipe/{recipeId}/rating", recipeRating);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
