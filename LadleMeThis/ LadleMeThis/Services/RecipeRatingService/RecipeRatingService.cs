using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatings;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Repositories.RecipeRepository;

namespace LadleMeThis.Services.RecipeRatingService;

public class RecipeRatingService(IRecipeRatingRepository recipeRatingRepository,IUserService userService)
{
	private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
	private readonly IUserService _userService = userService;
	
	public List<RecipeRatingDto> CreateRecipeRatingDtoList(ICollection<RecipeRating> ratings)
	{
		var users = _userService.GetUserDtos();

		return ratings.Select(r => CreateRecipeRatingDto(r, users)).ToList();
	}

	private RecipeRatingDto CreateRecipeRatingDto(RecipeRating rating, List<UserDto> users)
	{
		var user = users.FirstOrDefault(u => u.UserId == rating.UserId);
		
		if (user is null)
			user = new UserDto(null, "Jane Doe");
		
		
		return new RecipeRatingDto(
			rating.RatingId,
			rating.Review,
			rating.Rating,
			rating.DateCreated,
			user
		);
	}

	private async Task<List<RecipeRating>> GetRecipeRatingByIds(int recipeId) => 
		await _recipeRatingRepository.GetById(recipeId); 
}