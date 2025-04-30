using System.Net;
using System.Net.Http.Json;
using System.Text;
using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Models.UserModels;
using Newtonsoft.Json;

namespace LadleMeThisIntegrationTests;

public class UserControllerTests(LadleMeThisFactory factory) : IClassFixture<LadleMeThisFactory>
{
	private readonly HttpClient _client = factory.CreateClient();
	private readonly UserLogger _logger = new UserLogger(_client);
	
	
	[Fact]
	public async Task Register_ReturnsCreated()
	{
		// Arrange
		var newUser = new RegistrationRequest("test2@example.com", "test_example2", "Test@123");

		// Act
		var response = await _client.PostAsJsonAsync("/register", newUser);
		
		// Assert
		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.Created, response.StatusCode);
	}
	
	[Fact]
	public async Task Login_ReturnsOK_AttachesToken()
	{
		// Arrange
		const string userName = "test_example";
		const string password = "Test@123";
		var testUser = new AuthRequest(userName,password);
		
		// Act
		var response = await _client.PostAsJsonAsync("/login", testUser);
		var cookies = response.Headers.GetValues("Set-Cookie");
		var authToken = cookies.FirstOrDefault(c => c.Contains("AuthToken"));
		
		// Assert
		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.NotNull(authToken);
	}
	
	[Fact]
	public async Task GetUserById_ReturnsUserResponseDTO()
	{
		// Arrange
		const string email = "test_example";
		const string password = "Test@123";
		await _logger.LoginUser(email, password);
		
		// Act
		var response = await _client.GetAsync("user");
		var content = await response.Content.ReadFromJsonAsync<UserResponseDTO>();

		// Assert
		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.NotNull(content);
		Assert.Equal(email, content.Email);
	}
	
	[Fact]
	public async Task UpdateUser_ReturnsNoContent()
	{
		// Arrange
		const string email = "test_example";
		const string password = "Test@123";
		await _logger.LoginUser(email, password);

		var updatedTestUser = new UserUpdateDTO()
		{
			Username = "test@example",
			Email = "",
			NewPassword = ""
		};
		
		// Act
		var response = await _client.PutAsJsonAsync("/user/", updatedTestUser);
		var getResponse = await _client.GetAsync("/user/");
		var content = await getResponse.Content.ReadFromJsonAsync<UserResponseDTO>();
		
		// Assert
		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		Assert.NotNull(content);
		Assert.Equal(updatedTestUser.Username, content.Username);
	}
	
	[Fact]
	public async Task DeleteUser_ReturnsNoContent()
	{
		// Arrange
		const string email = "test_example";
		const string password = "Test@123";
		await _logger.LoginUser(email, password);
		
		// Act
		var response = await _client.DeleteAsync("user/");

		// Assert
		response.EnsureSuccessStatusCode();
		Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
	}
	
}