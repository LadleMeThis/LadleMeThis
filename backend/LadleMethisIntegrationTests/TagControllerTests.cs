using System.Net;
using System.Net.Http.Json;
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
        response.EnsureSuccessStatusCode(); // Status code 200-299
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
}