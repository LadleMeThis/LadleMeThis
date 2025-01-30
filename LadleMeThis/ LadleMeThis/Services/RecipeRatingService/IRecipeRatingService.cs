using LadleMeThis.Models.RecipeRatings;

namespace LadleMeThis.Services.RecipeRatingService;

public interface IRecipeRatingService
{
	List<RecipeRatingDto> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings);
}