using LadleMeThis.Data.Entity;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Services.RecipeRatingService;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LadleMeThisTests;

public class RecipeRatingServiceTests
{
	private Mock<IRecipeRatingRepository> _recipeRatingRepositoryMock;
	private Mock<IUserService> _userServiceMock;
	private RecipeRatingService _recipeRatingService;

	[SetUp]
	public void Setup()
	{
		_recipeRatingRepositoryMock = new Mock<IRecipeRatingRepository>();
		_userServiceMock = new Mock<IUserService>();
		_recipeRatingService = new RecipeRatingService(_recipeRatingRepositoryMock.Object, _userServiceMock.Object);
	}

	[Test]
	public async Task CreateRecipeRatingDtoList_ShouldReturnListOfRecipeRatingDTOs_WhenRatingsExist()
	{
		var ratings = new List<RecipeRating>
		{
			new RecipeRating
			{
				RatingId = 1,
				Review = "Great recipe!",
				Rating = 5,
				DateCreated = DateTime.UtcNow,
				User = new IdentityUser { Id = "user1" }
			},
			new RecipeRating
			{
				RatingId = 2,
				Review = "Not bad.",
				Rating = 3,
				DateCreated = DateTime.UtcNow,
				User = new IdentityUser { Id = "user2" }
			}
		};

		var users = new List<UserReviewDTO>
		{
			new UserReviewDTO { UserId = "user1", UserName = "John Doe" },
			new UserReviewDTO { UserId = "user2", UserName = "Jane Smith" }
		};

		_userServiceMock.Setup(service => service.GetAllUsersInReviewFormatAsync()).ReturnsAsync(users);

		var result = await _recipeRatingService.CreateRecipeRatingDtoList(ratings);

		Assert.That(result.Count, Is.EqualTo(2));
		Assert.Multiple(() =>
		{
			Assert.That(result[0].User.UserName, Is.EqualTo("John Doe"));
			Assert.That(result[0].Rating, Is.EqualTo(5));
			Assert.That(result[1].User.UserName, Is.EqualTo("Jane Smith"));
			Assert.That(result[1].Rating, Is.EqualTo(3));
		});
	}

	[Test]
	public async Task GetRecipeRatingByIds_ShouldReturnListOfRecipeRatings_WhenValidIdsArePassed()
	{
		var ratingIds = new[] { 1, 2 };
		var ratings = new List<RecipeRating>
		{
			new RecipeRating { RatingId = 1, Review = "Excellent!", Rating = 5 },
			new RecipeRating { RatingId = 2, Review = "Good.", Rating = 4 }
		};

		_recipeRatingRepositoryMock.Setup(repo => repo.GetByIds(ratingIds)).ReturnsAsync(ratings);

		var result = await _recipeRatingService.GetRecipeRatingByIds(ratingIds);

		Assert.That(result.Count, Is.EqualTo(2));
		Assert.Multiple(() =>
		{
			Assert.That(result[0].Review, Is.EqualTo("Excellent!"));
			Assert.That(result[0].Rating, Is.EqualTo(5));
			Assert.That(result[1].Review, Is.EqualTo("Good."));
			Assert.That(result[1].Rating, Is.EqualTo(4));
		});
	}

	[Test]
	public async Task CreateRecipeRatingDto_ShouldReturnDummyUser_WhenNoUserFound()
	{
		var ratings = new List<RecipeRating>
		{
			new RecipeRating
			{
				RatingId = 1,
				Review = "LGTM",
				Rating = 5,
				DateCreated = DateTime.UtcNow,
				User = new IdentityUser { Id = "nonexistentUser" }
			}
		};

		var users = new List<UserReviewDTO>
		{
			new UserReviewDTO { UserId = "user1", UserName = "John Doe" }
		};

		_userServiceMock.Setup(service => service.GetAllUsersInReviewFormatAsync()).ReturnsAsync(users);

		var result = await _recipeRatingService.CreateRecipeRatingDtoList(ratings);

		Assert.Multiple(() =>
		{
			Assert.That(result[0].User.UserName, Is.EqualTo("DummyUser"));
			Assert.That(result[0].Review, Is.EqualTo("LGTM"));
			Assert.That(result[0].Rating, Is.EqualTo(5));
		});
	}
}