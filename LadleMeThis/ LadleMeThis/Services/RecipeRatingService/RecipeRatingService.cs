using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRatingRepository;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.UserService;

namespace LadleMeThis.Services.RecipeRatingService;

public class RecipeRatingService(IRecipeRatingRepository recipeRatingRepository,IUserService userService)
{
	private readonly IRecipeRatingRepository _recipeRatingRepository = recipeRatingRepository;
	private readonly IUserService _userService = userService;
	
	public async Task<List<RecipeRatingDTO>> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings)
	{
		var users = await _userService.GetAllUsersAsync();

		return ratings.Select(r => CreateRecipeRatingDto(r, users)).ToList();
	}

	private RecipeRatingDTO CreateRecipeRatingDto(RecipeRating rating, IEnumerable<UserResponseDTO> users)
	{
		var user = users.FirstOrDefault(u => u.UserId == rating.UserId);
		
		if (user is null)
			user = new UserDTO(null, "Jane Doe");
		
		
		return new RecipeRatingDTO(
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