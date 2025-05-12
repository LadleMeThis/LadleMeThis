using System.Net;
using System.Net.Http.Json;
using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LadleMeThisIntegrationTests;

[Collection("IntegrationTests")]
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
    public async Task GetByIdAsync_ReturnsTag()
    {
        // Arrange
        var tag = await GetLast();

        // Act
        var response = await _client.GetAsync($"/tag/{tag.TagId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var returnedTag = await response.Content.ReadFromJsonAsync<TagDTO>();
        Assert.NotNull(returnedTag);
        Assert.Equal(tag.TagId, returnedTag.TagId);
        Assert.Equal(tag.Name, returnedTag.Name);
    }

    [Fact]
    public async Task DeleteByIdAsync_RemovesTag()
    {
        // Arrange
        var newTag = new TagCreateRequest { Name = "TestTag" };
        var response = await _client.PostAsJsonAsync("/tags", newTag);

        var tag = await GetLast();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/tag/{tag.TagId}");
        var getResponse = await _client.GetAsync($"/tag/{tag.TagId}");

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

        using var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

        var expectedTags = await context.Tags.ToListAsync();

        Assert.Equal(expectedTags.Count, responseContent.Count);
    }
    
    private async Task<Tag> GetLast()
    {
        using var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LadleMeThisContext>();

        var expectedTags = await context.Tags.ToListAsync();

        return expectedTags.Last();
    }
}