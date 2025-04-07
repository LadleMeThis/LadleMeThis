using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeRatingsModels;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.RecipeRatingService;

public interface IRecipeRatingService
{
	Task<List<RecipeRatingDTO>> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings);
	Task<List<RecipeRating>> GetRecipeRatingByIds(int[] ratingsIds);
	Task<int> CreateRecipeRating(CreateRecipeRatingDTO createRecipeRatingDto, IdentityUser user, Recipe recipe);
}