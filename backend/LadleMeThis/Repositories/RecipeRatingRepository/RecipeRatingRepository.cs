using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.RecipeRatingRepository;

public class RecipeRatingRepository(LadleMeThisContext ladleMeThisContext):IRecipeRatingRepository
{
	private readonly LadleMeThisContext _dbContext = ladleMeThisContext;

	public async Task<List<RecipeRating>> GetById(int recipeId) =>
		await _dbContext.Ratings.Where(r => r.Recipe.RecipeId == recipeId).ToListAsync();
	
	public async Task<List<RecipeRating>> GetByIds(int[] ratingIds) =>
		await _dbContext.Ratings
			.Where(r => ratingIds.Contains(r.RatingId))
			.ToListAsync();
	
	public async Task<int> Create(RecipeRating recipeRating)
	{
		if (recipeRating == null) throw new ArgumentNullException(nameof(recipeRating));

		try
		{
			_dbContext.Ratings.Add(recipeRating);
			await ladleMeThisContext.SaveChangesAsync();
		
			return recipeRating.RatingId;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error creating recipe rating: {ex.Message}");
			throw new InvalidOperationException("An error occurred while creating the recipe rating");
		}
	}

}