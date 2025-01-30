using LadleMeThis.Context;
using LadleMeThis.Models.RecipeModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.RecipeRepository;

public class RecipeRepository(LadleMeThisContext ladleMeThisContext):IRecipeRepository
{
	private readonly LadleMeThisContext _dbContext = ladleMeThisContext;
	
	public async Task<List<Recipe>> GetAll() => 
		await _dbContext.Recipes.ToListAsync();

	public async Task<List<Recipe>> GetByCategoryId(int categoryId) =>
		await _dbContext.Recipes
		.Where(r => r.Categories.Any(c => c.CategoryId == categoryId))
	.ToListAsync();
	

	public async Task<List<Recipe>> GetByTagId(int tagId) =>
		await _dbContext.Recipes
			.Where(r => r.Tags.Any(t => t.TagId == tagId))
			.ToListAsync();

	public async Task<List<Recipe>> GetByIngredientId(int ingredientId) => 
		await _dbContext.Recipes
		.Where(r => r.Ingredients.Any(i => i.IngredientId == ingredientId))
		.ToListAsync();

	public async Task<List<Recipe>> GetByUserId(int userId) => 
		await _dbContext.Recipes
			.Where(r => r.UserId == userId)
			.ToListAsync();

	public async Task<Recipe> GetByRecipeId(int recipeId) => 
		await _dbContext.Recipes
			.Where(r => r.RecipeId == recipeId)
			.FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Recipe not found");

	public async Task<int> Create(Recipe recipe)
	{
		try
		{
			_dbContext.Recipes.Add(recipe);
			await _dbContext.SaveChangesAsync();
			
			return recipe.RecipeId;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new KeyNotFoundException("Recipe not found");
		}
	}

	public async Task<bool> Update(Recipe recipe)
	{
		try
		{
			_dbContext.Recipes.Update(recipe);
			await _dbContext.SaveChangesAsync();
			
			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new KeyNotFoundException("Recipe not found");
		}
	}

	public async Task<bool> Delete(int recipeId)
	{
		try
		{
			var recipe = new Recipe { RecipeId = recipeId };
			
			 _dbContext.Entry(recipe).State = EntityState.Deleted;
			
			await _dbContext.SaveChangesAsync();
			
			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new KeyNotFoundException("Recipe not found");
		}
	}
	
	
}