using LadleMeThis.Models.RecipeRatingsModels;

namespace LadleMeThis.Services.RecipeRatingService;

public interface IRecipeRatingService
{
	List<RecipeRatingDTO> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings);
}