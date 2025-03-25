using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.UserService;

namespace LadleMeThis.Services.RecipeRatingService;

public class RecipeRatingService(IRecipeRatingRepository recipeRatingRepository,IUserService userService):IRecipeRatingService
{
	private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
	private readonly IUserService _userService = userService;
	private static readonly UserReviewDTO DummyUser = new UserReviewDTO
	{
		UserId = "dummyUs3r1d",
		UserName = "DummyUser"
	};
	
	public async Task<List<RecipeRatingDTO>> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings)
	{
		var users = await _userService.GetAllUsersInReviewFormatAsync();

		return ratings.Select(r => CreateRecipeRatingDto(r, users)).ToList();
	}

	public async Task<List<RecipeRating>> GetRecipeRatingByIds(int[] ratingsIds) => 
		await _recipeRatingRepository.GetByIds(ratingsIds);

	private RecipeRatingDTO CreateRecipeRatingDto(RecipeRating rating, IEnumerable<UserReviewDTO> users)
	{
		var user = users.FirstOrDefault(u => u.UserId == rating.User.Id);
		
		return new RecipeRatingDTO(
			rating.RatingId,
			rating.Review,
			rating.Rating,
			rating.DateCreated,
			user ?? DummyUser
		);
	}

	private async Task<List<RecipeRating>> GetRecipeRatingByIds(int recipeId) => 
		await _recipeRatingRepository.GetById(recipeId); 
}