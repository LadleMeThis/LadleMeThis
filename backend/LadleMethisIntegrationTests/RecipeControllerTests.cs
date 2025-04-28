using LadleMeThisIntegrationTests;
using System.Net.Http.Json;
using System.Net;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Azure.Identity;

namespace LadleMethisIntegrationTests
{
    public class RecipeControllerTests : IClassFixture<LadleMeThisFactory>
    {
        private readonly HttpClient _client;
        private readonly UserLogger _userLogger;
        private readonly LadleMeThisFactory _factory;
        private  const string EMAIL = "admin@example.com";
        private const string PASSWORD = "Admin@123";

        public RecipeControllerTests(LadleMeThisFactory factory)
        {
            _client = factory.CreateClient();
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
        public async Task GetRecipesByCategoryId_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            int categoryId = -1;
            var response = await _client.GetAsync($"/recipes/category/{categoryId}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

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
           await  _userLogger.LoginUser(EMAIL, PASSWORD);


            // Act
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
    }
}
