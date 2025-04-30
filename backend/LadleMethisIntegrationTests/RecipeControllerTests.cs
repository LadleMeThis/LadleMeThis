using LadleMeThisIntegrationTests;
using System.Net.Http.Json;
using System.Net;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LadleMethisIntegrationTests
{
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

            await _userLogger.LoginUser(VALID_EMAIL,VALID_PASSWORD);

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
            Assert.Single(recipes);

            Assert.Equal(recipeName, recipes.First().Name);
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
                    .Where(r => r.Ingredients.Any(i =>  ingredientIds.Contains(i.IngredientId)))
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


    }
}
