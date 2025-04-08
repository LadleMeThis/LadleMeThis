using LadleMeThis.Data.Entity;

namespace LadleMeThis.Repositories.RecipeRatingRepository;

public interface IRecipeRatingRepository
{
	Task<List<RecipeRating>> GetById(int recipeId);
	Task<List<RecipeRating>> GetByIds(int[] ratingIds);
	Task<int> Create(RecipeRating recipeRating);
}