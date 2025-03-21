using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeRatingsModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.RecipeRatingRepository;

public class RecipeRatingRepository(LadleMeThisContext ladleMeThisContext):IRecipeRatingRepository
{
	private readonly LadleMeThisContext _dbContext = ladleMeThisContext;

	public async Task<List<RecipeRating>> GetById(int recipeId) =>
		await _dbContext.Ratings.Where(r => r.RecipeId == recipeId).ToListAsync();
	
	public async Task<List<RecipeRating>> GetByIds(int[] ratingIds) =>
		await _dbContext.Ratings
			.Where(r => ratingIds.Contains(r.RatingId))
			.ToListAsync();

}