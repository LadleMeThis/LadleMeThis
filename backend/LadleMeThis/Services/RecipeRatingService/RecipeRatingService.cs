using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Identity;

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
	
	public async Task<int> CreateRecipeRating(CreateRecipeRatingDTO createRecipeRatingDto, IdentityUser user, Recipe recipe)
	{
		var recipeRating = CreateRecipeRatingEntity(createRecipeRatingDto, user, recipe);
		return await _recipeRatingRepository.Create(recipeRating);
	}

	private async Task<List<RecipeRating>> GetRecipeRatingByIds(int recipeId) => 
		await _recipeRatingRepository.GetById(recipeId);

	private RecipeRating CreateRecipeRatingEntity(CreateRecipeRatingDTO createRecipeRatingDto, IdentityUser user, Recipe recipe) =>
		new RecipeRating()
		{
			Review = createRecipeRatingDto.Review,
			Rating = createRecipeRatingDto.Rating,
			Recipe = recipe,
			User = user,
			DateCreated = DateTime.Today
		};


}