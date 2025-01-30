using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeService;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.RecipeController;

[Route("/[controller]")]
[ApiController]
public class RecipeController(IRecipeService recipeService, IUserService userService) : ControllerBase
{
	private readonly IRecipeService _recipeService = recipeService;
	private readonly IUserService _userService = userService;
		
	
	[HttpGet("/recipes")]
	public async Task<ActionResult<List<RecipeCardDto>>> GetAllRecipes()
	{
		try
		{
			var recipes = await _recipeService.GetALlRecipeCards();
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpGet("/recipes/by-category/{categoryId}")]
	public async Task<ActionResult<List<RecipeCardDto>>> GetRecipesByCategoryId(int categoryId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByCategoryId(categoryId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpGet("/recipes/by-tag/{tagId}")]
	public async Task<ActionResult<List<RecipeCardDto>>> GetRecipesByTagId(int tagId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByTagId(tagId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpGet("/recipes/by-ingredient/{ingredientId}")]
	public async Task<ActionResult<List<RecipeCardDto>>> GetRecipesByIngredientId(int ingredientId)
	{
		try
		{
			var recipes = await _recipeService.GetRecipesByIngredientId(ingredientId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpGet("/recipes/by-id/{recipeId}")]
	public async Task<ActionResult<Recipe>> GetRecipeByRecipeId(int recipeId)
	{
		try
		{
			var recipe = await _recipeService.GetRecipeByRecipeId(recipeId);
			return Ok(recipe);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpPost("/recipes")]
	public async Task<ActionResult<int>> CreateRecipe(CreateRecipeDto createRecipeDto)
	{
		try
		{
			var user = _userservice.GetCurrentUser(); 
			var recipeId = await _recipeService.Create(createRecipeDto, user);
			return Ok(recipeId);
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpPut("/recipes/{recipeId}")]
	public async Task<ActionResult> UpdateRecipe(int recipeId, UpdateRecipeDto updateRecipeDto)
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
			return StatusCode(500, "Internal server error");
		}
	}
	
	[HttpDelete("/recipes/{recipeId}")]
	public async Task<ActionResult> DeleteRecipe(int recipeId)
	{
		try
		{ 
			await _recipeService.DeleteRecipe(recipeId);

			return NoContent();
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Internal server error");
		}
	}
}