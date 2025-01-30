using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatings;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;

namespace LadleMeThis.Services.RecipeService;

public class RecipeService(IRecipeRepository recipeRepository, IRecipeDetailsService recipeDetailsService):IRecipeService
{
	private readonly IRecipeRepository _repository = recipeRepository;
	private readonly IRecipeDetailsService _recipeDetailsService = recipeDetailsService;

	public async Task<List<RecipeCardDto>> GetALlRecipeCards()
	{
		var recipes = await _repository.GetAll();
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDto>> GetRecipesByCategoryId(int categoryId)
	{
		var recipes = await _repository.GetByCategoryId(categoryId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDto>> GetRecipesByTagId(int tagId)
	{
		var recipes = await _repository.GetByTagId(tagId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDto>> GetRecipesByIngredientId(int ingredientId)
	{
		var recipes = await _repository.GetByIngredientId(ingredientId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<Recipe> GetRecipeByRecipeId(int recipeId) =>
		await _repository.GetByRecipeId(recipeId);
	
	public async Task<bool> DeleteRecipe(int recipeId) => 
		await _repository.Delete(recipeId);

	public async Task<int> Create(CreateRecipeDto createRecipeDto, User user)
	{
		var recipe = CreateRecipe(createRecipeDto, user);
		
		return await _repository.Create(recipe);
	}

	public async Task<bool> UpdateRecipe(UpdateRecipeDto updateRecipeDto)
	{
		var recipe = await _repository.GetByRecipeId(updateRecipeDto.RecipeId);

		if (updateRecipeDto.Name != null)
			recipe.Name = updateRecipeDto.Name;

		if (updateRecipeDto.Instructions != null)
			recipe.Instructions = updateRecipeDto.Instructions;

		if (updateRecipeDto.PrepTime.HasValue)
			recipe.PrepTime = updateRecipeDto.PrepTime.Value;

		if (updateRecipeDto.CookTime.HasValue)
			recipe.CookTime = updateRecipeDto.CookTime.Value;

		if (updateRecipeDto.ServingSize.HasValue)
			recipe.ServingSize = updateRecipeDto.ServingSize.Value;

		if (updateRecipeDto.Tags != null)
			recipe.Tags = await GetUpdatedTags(updateRecipeDto.Tags);

		if (updateRecipeDto.Ingredients != null)
			recipe.Ingredients = await GetUpdatedIngredients(updateRecipeDto.Ingredients);

		if (updateRecipeDto.Categories != null)
			recipe.Categories = await GetUpdatedCategories(updateRecipeDto.Categories);

		return await _repository.Update(recipe);
	}

	private async Task<List<Tag>> GetUpdatedTags(int[]? updatedTagIds) =>
		await _recipeDetailsService.GetTagsByIds(int[] updatedTagIds);

	private async Task<List<Ingredient>> GetUpdatedIngredients(int[] updatedIngredientIds) =>
		await _recipeDetailsService.GetIngredientsByIds(int[] updatedIngredientIds);

	private async Task<List<Category>> GetUpdatedCategories(int[]? updatedCategoryIds) =>
		await _recipeDetailsService.GetCategoriesByIds(int[] updatedCategoryIds);

	private RecipeCardDto CreateRecipeCard(Recipe recipe)
	{
		var averageRating = recipe.Ratings.Any() 
			? (int)recipe.Ratings.Average(r => r.Rating)
			: 0;
		var fullTime = recipe.PrepTime + recipe.CookTime;

		return new RecipeCardDto(
			recipe.RecipeId,
			recipe.Name,
			fullTime,
			recipe.ServingSize,
			averageRating,
			recipe.Tags?.ToArray() ?? Array.Empty<Tag>(),
			recipe.Categories?.ToArray() ?? Array.Empty<Category>()
		);
	}

	private Recipe CreateRecipe(CreateRecipeDto recipeDto, User user)
	{
		var tags = _recipeDetailsService.GetTagsByIds(recipeDto.Tags);
		var ingredients = _recipeDetailsService.GetTagsByIds(recipeDto.Ingredients);
		var categories = _recipeDetailsService.GetTagsByIds(recipeDto.Categories);

		return new Recipe()
		{
			Name = recipeDto.Name,
			Instructions = recipeDto.Instructions,
			PrepTime = recipeDto.PrepTime,
			CookTime = recipeDto.CookTime,
			ServingSize = recipeDto.ServingSize,
			UserId = user.UserId,
			User = user,
			Categories = categories,
			Tags = tags,
			Ingredients = ingredients
		};
	}
}