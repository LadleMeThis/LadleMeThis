using System.Net;
using System.Net.Http.Json;
using System.Text;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.TagModels;

namespace LadleMeThisIntegrationTests;

public class TagControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    

    [Fact]
    public async Task AddAsync_CreatesTag_AndReturnsIt()
    {
        // Arrange
        var newTag = new TagCreateRequest { Name = "TestTag" };

        // Act
        var response = await _client.PostAsJsonAsync("/tags", newTag);

        // Assert
        response.EnsureSuccessStatusCode(); 
        var createdTag = await response.Content.ReadFromJsonAsync<TagDTO>();
        Assert.NotNull(createdTag);
        Assert.Equal("TestTag", createdTag.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTag_AfterAddingIt()
    {
        // Arrange
        var newTag = new TagCreateRequest { Name = "AnotherTag" };
        var createResponse = await _client.PostAsJsonAsync("/tags", newTag);
        var createdTag = await createResponse.Content.ReadFromJsonAsync<TagDTO>();

        // Act
        var response = await _client.GetAsync($"/tag/{createdTag.TagId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedTag = await response.Content.ReadFromJsonAsync<TagDTO>();
        Assert.Equal(createdTag.TagId, returnedTag.TagId);
        Assert.Equal("AnotherTag", returnedTag.Name);
    }

    [Fact]
    public async Task DeleteByIdAsync_RemovesTag()
    {
        // Arrange
        var newTag = new TagCreateRequest { Name = "TagToDelete" };
        var createResponse = await _client.PostAsJsonAsync("/tags", newTag);
        var createdTag = await createResponse.Content.ReadFromJsonAsync<TagDTO>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/tag/{createdTag.TagId}");
        var getResponse = await _client.GetAsync($"/tag/{createdTag.TagId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task DeleteByIdAsync_Fails()
    {
        // Arrange
        const int tagId = -1;
        
        // Act
        var deleteResponse = await _client.DeleteAsync($"/tag/{tagId}");
        var responseContent = await deleteResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        Assert.Equal(ErrorMessages.NotFoundMessage, responseContent);
    }
    
    [Fact]
    public async Task AddAsync_Fails()
    {
        // Arrange
        var newTag = new TagCreateRequest {  };

        // Act
        var response = await _client.PostAsJsonAsync("/tags", newTag);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("validation error", responseContent, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsAllTags()
    {
        // Act
        var response = await _client.GetAsync("/tags");
        var responseContent = await response.Content.ReadFromJsonAsync<List<TagDTO>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<List<TagDTO>>(responseContent);
    }
}