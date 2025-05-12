using System.Net;
using System.Net.Http.Json;
using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.ErrorMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LadleMeThisIntegrationTests;

[Collection("IntegrationTests")]
public class CategoryControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
{
	private readonly HttpClient _client = factory.CreateClient();


	[Fact]
	public async Task AddAsync_CreatesTag_AndReturnsIt()
	{
		// Arrange
		var newCategory = new CategoryCreateRequest { Name = "TestIngredient" };

		// Act
		var response = await _client.PostAsJsonAsync("/categories", newCategory);

		// Assert
		response.EnsureSuccessStatusCode();
		var createdCategory = await response.Content.ReadFromJsonAsync<CategoryDTO>();
		Assert.NotNull(createdCategory);
		Assert.Equal("TestIngredient", createdCategory.Name);
	}

	[Fact]
	public async Task GetByIdAsync_ReturnsCategory()
	{
		// Arrange
		 var category = await GetLast();

		// Act
		var response = await _client.GetAsync($"/category/{category.CategoryId}");

		// Assert
		response.EnsureSuccessStatusCode();
		var returnedCategory = await response.Content.ReadFromJsonAsync<CategoryDTO>();
		Assert.NotNull(returnedCategory);
		Assert.Equal(category.CategoryId, returnedCategory.CategoryId);
		Assert.Equal(category.Name, returnedCategory.Name);
	}

	[Fact]
	public async Task DeleteByIdAsync_RemovesTag()
	{
		// Arrange
		var category = await GetLast();

		// Act
		var deleteResponse = await _client.DeleteAsync($"/category/{category.CategoryId}");
		var getResponse = await _client.GetAsync($"/category/{category.CategoryId}");

		// Assert
		Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
		Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
	}
	
	[Fact]
	public async Task DeleteByIdAsync_Fails()
	{
		// Arrange
		const int categoryId = -1;

		// Act
		var deleteResponse = await _client.DeleteAsync($"/category/{categoryId}");
		var responseContent = await deleteResponse.Content.ReadAsStringAsync();

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
		Assert.Equal(ErrorMessages.NotFoundMessage, responseContent);
	}

	[Fact]
	public async Task AddAsync_Fails()
	{
		// Arrange
		var newCategory = new CategoryCreateRequest { };

		// Act
		var response = await _client.PostAsJsonAsync("/categories", newCategory);
		var responseContent = await response.Content.ReadAsStringAsync();

		// Assert
		Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		Assert.Contains("validation error", responseContent, StringComparison.OrdinalIgnoreCase);
	}

	[Fact]
	public async Task GetAllAsync_ReturnsAllCategories()
	{
		// Act
		var response = await _client.GetAsync("/categories");
		var responseContent = await response.Content.ReadFromJsonAsync<List<CategoryDTO>>();

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.IsType<List<CategoryDTO>>(responseContent);
		Assert.NotEmpty(responseContent);

		using var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

		var expectedCategories = await context.Categories.ToListAsync();

		Assert.Equal(expectedCategories.Count, responseContent.Count);
	}
	
	private async Task<Category> GetLast()
	{
		using var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

		var expectedCategories = await context.Categories.ToListAsync();

		return expectedCategories.Last();
	}
}