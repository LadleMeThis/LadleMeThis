using System.Net;
using System.Net.Http.Json;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.ErrorMessages;

namespace LadleMeThisIntegrationTests;

public class CategoryControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
{
	private readonly HttpClient _client = factory.CreateClient();
	
	
	
[Fact]
    public async Task AddAsync_CreatesTag_AndReturnsIt()
    {
        // Arrange
        var newCategory= new CategoryCreateRequest { Name = "TestIngredient" };

        // Act
        var response = await _client.PostAsJsonAsync("/categories", newCategory);

        // Assert
        response.EnsureSuccessStatusCode(); 
        var createdCategory = await response.Content.ReadFromJsonAsync<CategoryDTO>();
        Assert.NotNull(createdCategory);
        Assert.Equal("TestIngredient", createdCategory.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTag_AfterAddingIt()
    {
        // Arrange
        var newCategory= new CategoryCreateRequest { Name = "AnotherCategory" };
        var createResponse = await _client.PostAsJsonAsync("/categories", newCategory);
        var createdCategory = await createResponse.Content.ReadFromJsonAsync<CategoryDTO>();

        // Act
        var response = await _client.GetAsync($"/category/{createdCategory.CategoryId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedCategory = await response.Content.ReadFromJsonAsync<CategoryDTO>();
        Assert.Equal(createdCategory.CategoryId, returnedCategory.CategoryId);
        Assert.Equal("AnotherCategory", returnedCategory.Name);
    }

    [Fact]
    public async Task DeleteByIdAsync_RemovesTag()
    {
        // Arrange
        var newCategory= new CategoryCreateRequest { Name = "CategoryToDelete" };
        var createResponse = await _client.PostAsJsonAsync("/categories", newCategory);
        var createdCategory = await createResponse.Content.ReadFromJsonAsync<CategoryDTO>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/category/{createdCategory.CategoryId}");
        var getResponse = await _client.GetAsync($"/category/{createdCategory.CategoryId}");

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
        var newCategory= new CategoryCreateRequest {  };

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
    }
}