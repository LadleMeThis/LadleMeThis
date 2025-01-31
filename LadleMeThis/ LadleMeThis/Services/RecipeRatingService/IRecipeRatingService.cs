using LadleMeThis.Models.RecipeRatingsModels;

namespace LadleMeThis.Services.RecipeRatingService;

public interface IRecipeRatingService
{
	Task<List<RecipeRatingDTO>> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings);
	Task<List<RecipeRating>> GetRecipeRatingByIds(int[] ratingsIds);
}