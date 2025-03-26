using System.Security.Claims;
using LadleMeThis.Data.Entity;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeService;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.RecipeController;


[ApiController]
public class RecipeController(IRecipeService recipeService) : ControllerBase
{
	[HttpGet("/recipes")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetAllRecipes()
	{
		try
		{
			var recipes = await recipeService.GetAllRecipeCards();
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
			var recipes = await recipeService.GetRecipesByCategoryId(categoryId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}

    [HttpGet("/recipes/{recipeName}")]
    public async Task<ActionResult<List<RecipeCardDTO>>> GetRecipesByName(string recipeName)
    {
        try
        {
            var recipes = await recipeService.GetRecipesByName(recipeName);
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
			var recipes = await recipeService.GetRecipesByTagId(tagId);
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
			var recipes = await recipeService.GetRecipesByIngredientId(ingredientId);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpGet($"/recipes/ingredients")]
	public async Task<ActionResult<List<RecipeCardDTO>>> GetRecipesByIngredientIds([FromQuery] List<int> ingredientIds)
	{
		if (ingredientIds == null || !ingredientIds.Any())
		{
			return BadRequest("Ingredient IDs must be provided.");
		}

		try
		{
			var recipes = await recipeService.GetRecipesByIngredientIds(ingredientIds);
			return Ok(recipes);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	
	[HttpGet("/recipe/{recipeId:int}")]
	public async Task<ActionResult<FullRecipeDTO>> GetRecipeByRecipeId(int recipeId)
	{
		try
		{
			var recipe = await recipeService.GetRecipeByRecipeId(recipeId);
			return Ok(recipe);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
	[Authorize]
	[HttpPost("/recipes")]
	public async Task<ActionResult<int>> CreateRecipe(CreateRecipeDTO createRecipeDto)
	{
		try
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var recipeId = await recipeService.Create(createRecipeDto, userId);
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
			

			await recipeService.UpdateRecipe(updateRecipeDto);

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
			await recipeService.DeleteRecipe(recipeId);

			return NoContent();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}
}