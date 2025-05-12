using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Services.TokenService;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;

namespace LadleMeThisTests
{
    public class UserServiceTests
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object, null, null, null, null);

            _tokenServiceMock = new Mock<ITokenService>();

            _userService = new UserService(_userManagerMock.Object, _roleManagerMock.Object, _tokenServiceMock.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsUsers()
        {
            var users = new List<IdentityUser>
    {
        new IdentityUser { Id = "1", UserName = "user1", Email = "email1@test.com" },
        new IdentityUser { Id = "2", UserName = "user2", Email = "email2@test.com" }
    };

            var mockUserQueryable = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(x => x.Users).Returns(mockUserQueryable);

            var result = await _userService.GetAllUsersAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Username, Is.EqualTo("user1"));
        }

        [Test]
        public async Task GetUserByIdAsync_UserExists_ReturnsUser()
        {
            var user = new IdentityUser { Id = "1", UserName = "user1", Email = "email@test.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);

            var result = await _userService.GetUserByIdAsync("1");

            Assert.That(result.UserId, Is.EqualTo("1"));
            Assert.That(result.Username, Is.EqualTo("user1"));
        }

        [Test]
        public void GetUserByIdAsync_UserNotFound_Throws()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync((IdentityUser)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.GetUserByIdAsync("1"));
        }

        [Test]
        public async Task CreateUserAsync_Success()
        {
            var request = new RegistrationRequest
                (
                 "newuser",
                 "test@example.com",
                 "StrongPassword123!"
                );

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), request.Password))
                            .ReturnsAsync(IdentityResult.Success);

            var result = await _userService.CreateUserAsync(request);

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task DeleteUserAsync_UserFound_ReturnsSuccess()
        {
            var user = new IdentityUser { Id = "1" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userService.DeleteUserAsync("1");

            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public void DeleteUserAsync_UserNotFound_Throws()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync((IdentityUser)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.DeleteUserAsync("1"));
        }

        [Test]
        public async Task RegisterAsync_InvalidRole_ReturnsFailedResult()
        {
            var request = new RegistrationRequest
                (
                 "newuser",
                 "test@example.com",
                 "StrongPassword123!"
                );

            _roleManagerMock.Setup(x => x.RoleExistsAsync("Admin")).ReturnsAsync(false);

            var result = await _userService.RegisterAsync(request, "Admin");

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessages.ContainsKey("RoleError"), Is.True);
        }

        [Test]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
            var user = new IdentityUser { Email = "user@test.com", UserName = "user" };
            _userManagerMock.Setup(x => x.FindByEmailAsync("user@test.com")).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "correct")).ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.CreateToken(user)).ReturnsAsync("mock-token");

            var result = await _userService.LoginAsync(new AuthRequest
                (
                  "user@test.com",
                  "correct"
                ));

            Assert.That(result.Success, Is.True);
            Assert.That(result.Token, Is.EqualTo("mock-token"));
        }

        [Test]
        public async Task LoginAsync_InvalidPassword_ReturnsFailure()
        {
            var user = new IdentityUser { Email = "user@test.com", UserName = "user" };
            _userManagerMock.Setup(x => x.FindByEmailAsync("user@test.com")).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "wrong")).ReturnsAsync(false);

            var result = await _userService.LoginAsync(new AuthRequest
                (
                  "user@test.com",
                  "wrong"
                ));

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessages.ContainsKey("Bad credentials"), Is.True);
        }

        [Test]
        public async Task LoginAsync_InvalidEmail_ReturnsFailure()
        {
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);
            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var result = await _userService.LoginAsync(new AuthRequest
                (
                  "not found",
                  "whatever"
                ));

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessages.ContainsKey("Bad credentials"), Is.True);
        }

        // Helper to mock IQueryable<IdentityUser>
        private Mock<Microsoft.EntityFrameworkCore.DbSet<IdentityUser>> GetMockDbSet(IQueryable<IdentityUser> data)
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<IdentityUser>>();
            mockSet.As<IQueryable<IdentityUser>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<IdentityUser>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<IdentityUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<IdentityUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}
