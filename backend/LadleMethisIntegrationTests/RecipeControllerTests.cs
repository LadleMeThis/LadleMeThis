using LadleMeThisIntegrationTests;
using System.Net.Http.Json;
using System.Net;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LadleMethisIntegrationTests
{
    public class RecipeControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly WebApplicationFactory<Program> _factory = factory;


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



    }
}
