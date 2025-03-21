using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeDetailService;
using LadleMeThis.Services.RecipeRatingService;

namespace LadleMeThis.Services.RecipeService;

public class RecipeService(IRecipeRepository recipeRepository, IRecipeDetailService recipeDetailsService, IRecipeRatingService recipeRatingService):IRecipeService
{
	private readonly IRecipeRepository _repository = recipeRepository;
	private readonly IRecipeDetailService _recipeDetailsService = recipeDetailsService;
	private readonly IRecipeRatingService _recipeRatingService = recipeRatingService;
	

	public async Task<List<RecipeCardDTO>> GetALlRecipeCards()
	{
		var recipes = await _repository.GetAll();
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDTO>> GetRecipesByCategoryId(int categoryId)
	{
		var recipes = await _repository.GetByCategoryId(categoryId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDTO>> GetRecipesByTagId(int tagId)
	{
		var recipes = await _repository.GetByTagId(tagId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}
	
	public async Task<List<RecipeCardDTO>> GetRecipesByIngredientId(int ingredientId)
	{
		var recipes = await _repository.GetByIngredientId(ingredientId);
		
		if (recipes.Count == 0)
			return [];
		
		return recipes.Select(CreateRecipeCard).ToList();
	}

	public async Task<FullRecipeDTO> GetRecipeByRecipeId(int recipeId, User user)
	{
		var recipe = await _repository.GetByRecipeId(recipeId);
		
		return await CreateFullRecipeDto(recipe, user);
	}

	
	public async Task<bool> DeleteRecipe(int recipeId) => 
		await _repository.Delete(recipeId);

	public async Task<int> Create(CreateRecipeDTO createRecipeDto, User user)
	{
		var recipe = await CreateRecipe(createRecipeDto, user);
		
		return await _repository.Create(recipe);
	}

	public async Task<bool> UpdateRecipe(UpdateRecipeDTO updateRecipeDto)
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

	private async Task<List<Tag>> GetUpdatedTags(int[] updatedTagIds) =>
		(List<Tag>)await _recipeDetailsService.GetTagsByIds(updatedTagIds);

	private async Task<List<Ingredient>> GetUpdatedIngredients(int[] updatedIngredientIds) =>
		(List<Ingredient>)await _recipeDetailsService.GetIngredientsByIds(updatedIngredientIds);

	private async Task<List<Category>> GetUpdatedCategories(int[] updatedCategoryIds) =>
		(List<Category>)await _recipeDetailsService.GetCategoriesByIds(updatedCategoryIds);
	private async Task<List<RecipeRating>> GetUpdatedRating(int[] updatedRatingsIds) =>
		 await _recipeRatingService.GetRecipeRatingByIds(updatedRatingsIds);

	private RecipeCardDTO CreateRecipeCard(Recipe recipe)
	{
		var averageRating = recipe.Ratings.Any() 
			? (int)recipe.Ratings.Average(r => r.Rating)
			: 0;
		var fullTime = recipe.PrepTime + recipe.CookTime;

		return new RecipeCardDTO(
			recipe.RecipeId,
			recipe.Name,
			fullTime,
			recipe.ServingSize,
			averageRating,
			recipe.Tags?.ToArray() ?? Array.Empty<Tag>(),
			recipe.Categories?.ToArray() ?? Array.Empty<Category>()
		);
	}

	private async Task<Recipe> CreateRecipe(CreateRecipeDTO recipeDto, User user)
	{
		var tags = await _recipeDetailsService.GetTagsByIds(recipeDto.Tags);
		var ingredients = await _recipeDetailsService.GetIngredientsByIds(recipeDto.Ingredients);
		var categories = await _recipeDetailsService.GetCategoriesByIds(recipeDto.Categories);

		return new Recipe()
		{
			Name = recipeDto.Name,
			Instructions = recipeDto.Instructions,
			PrepTime = recipeDto.PrepTime,
			CookTime = recipeDto.CookTime,
			ServingSize = recipeDto.ServingSize,
			UserId = user.UserId,
			User = user,
			Categories = categories.ToList(),
			Tags = tags.ToList(),
			Ingredients = ingredients.ToList()
		};
	}
	
	private async Task<FullRecipeDTO> CreateFullRecipeDto(Recipe recipe, User user)
	{
		var tags =  _recipeDetailsService.GetTagDTOsByTags(recipe.Tags);
		var ingredients =  _recipeDetailsService.GetIngredientDTOsByIngredients(recipe.Ingredients);
		var categories = _recipeDetailsService.GetCategoryDTOsByCategories(recipe.Categories);
		var ratings = await _recipeRatingService.CreateRecipeRatingDtoList(recipe.Ratings);

		return new FullRecipeDTO(
			recipe.RecipeId,
			recipe.Name,
			recipe.Instructions,
			recipe.PrepTime,
			recipe.CookTime,
			recipe.ServingSize,
			user.UserId,
			categories.ToList(),
			tags.ToList(),
			ingredients.ToList(),
			ratings
			);
	} 
}