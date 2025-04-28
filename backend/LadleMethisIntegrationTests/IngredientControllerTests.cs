using System.Net;
using System.Net.Http.Json;
using LadleMeThis.Context;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.IngredientsModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThisIntegrationTests;

public class IngredientControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
{
	private readonly HttpClient _client = factory.CreateClient();
	
	
	
[Fact]
    public async Task AddAsync_CreatesTag_AndReturnsIt()
    {
        // Arrange
        var newIngredient= new IngredientCreateRequest { Name = "TestIngredient", Unit = "test" };

        // Act
        var response = await _client.PostAsJsonAsync("/ingredients", newIngredient);

        // Assert
        response.EnsureSuccessStatusCode(); 
        var createdIngredient = await response.Content.ReadFromJsonAsync<IngredientDTO>();
        Assert.NotNull(createdIngredient);
        Assert.Equal("TestIngredient", createdIngredient.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTag_AfterAddingIt()
    {
        // Arrange
        var newIngredient= new IngredientCreateRequest { Name = "AnotherIngredient", Unit = "test"};
        var createResponse = await _client.PostAsJsonAsync("/ingredients", newIngredient);
        var createdIngredient = await createResponse.Content.ReadFromJsonAsync<IngredientDTO>();

        // Act
        var response = await _client.GetAsync($"/ingredient/{createdIngredient.IngredientId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedCategory = await response.Content.ReadFromJsonAsync<IngredientDTO>();
        Assert.Equal(createdIngredient.IngredientId, returnedCategory.IngredientId);
        Assert.Equal("AnotherIngredient", returnedCategory.Name);
    }

    [Fact]
    public async Task DeleteByIdAsync_RemovesTag()
    {
        // Arrange
        var newIngredient= new IngredientCreateRequest { Name = "IngredientToDelete", Unit = "Test"};
        var createResponse = await _client.PostAsJsonAsync("/ingredients", newIngredient);
        var createdIngredient = await createResponse.Content.ReadFromJsonAsync<IngredientDTO>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/ingredient/{createdIngredient.IngredientId}");
        var getResponse = await _client.GetAsync($"/ingredient/{createdIngredient.IngredientId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent ,deleteResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task DeleteByIdAsync_Fails()
    {
        // Arrange
        const int ingredientId = -1;
        
        // Act
        var deleteResponse = await _client.DeleteAsync($"/ingredient/{ingredientId}");
        var responseContent = await deleteResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        Assert.Equal(ErrorMessages.NotFoundMessage, responseContent);
    }
    
    [Fact]
    public async Task AddAsync_Fails()
    {
        // Arrange
        var newIngredient= new IngredientCreateRequest {  };

        // Act
        var response = await _client.PostAsJsonAsync("/ingredients", newIngredient);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("validation error", responseContent, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnAllIngredients()
    {
        
        // Act
        var response = await _client.GetAsync("/ingredients");
        var responseContent = await response.Content.ReadFromJsonAsync<List<IngredientDTO>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<List<IngredientDTO>>(responseContent);
    }
}