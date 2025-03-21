using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeService;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.RecipeController;

[ApiController]
public class RecipeController(IRecipeService recipeService, IUserService userService) : ControllerBase
{
	private readonly IRecipeService _recipeService = recipeService;
	private readonly IUserService _userService = userService;
		
	
	[HttpGet("/recipes")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetAllRecipes()
	{
		try
		{
			var recipes = await _recipeService.GetALlRecipeCards();
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}
	
	[HttpGet("/recipes/category/{categoryId}")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetRecipesByCategoryId(int categoryId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByCategoryId(categoryId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}
	
	[HttpGet("/recipes/tag/{tagId}")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetRecipesByTagId(int tagId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByTagId(tagId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpGet("/recipes/ingredient/{ingredientId}")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetRecipesByIngredientId(int ingredientId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByIngredientId(ingredientId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpGet("/recipe/{recipeId}")]
	public async Task<ActionResult<Recipe>> GetRecipeByRecipeId(int recipeId)
	{
		try
		{
			var user = new User();
			var recipe = await _recipeService.GetRecipeByRecipeId(recipeId, user);
			return Ok(recipe);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpPost("/recipes/{userId:int}")]
	public async Task<ActionResult<int>> CreateRecipe(CreateRecipeDTO createRecipeDto)
	{
		try
		{
			var user = new User();
			var recipeId = await _recipeService.Create(createRecipeDto, user);
			return Ok(recipeId);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}
	
	[HttpPut("/recipe/{recipeId}")]
	public async Task<ActionResult> UpdateRecipe(int recipeId, UpdateRecipeDTO updateRecipeDto)
	{
		try
		{
			if (recipeId != updateRecipeDto.RecipeId)
				return BadRequest("Recipe ID mismatch");
			

			await _recipeService.UpdateRecipe(updateRecipeDto);

			return NoContent();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpDelete("/recipe/{recipeId}")]
	public async Task<ActionResult> DeleteRecipe(int recipeId)
	{
		try
		{ 
			await _recipeService.DeleteRecipe(recipeId);

			return NoContent();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
}