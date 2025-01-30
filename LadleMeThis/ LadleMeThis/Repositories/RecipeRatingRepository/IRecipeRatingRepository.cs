using LadleMeThis.Models.RecipeRatings;

namespace LadleMeThis.Repositories.RecipeRatingRepository;

public interface IRecipeRatingRepository
{
	Task<List<RecipeRating>> GetById(int recipeId);
}