using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.SavedRecipesModels;
using LadleMeThis.Services.SavedRecipeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.SavedRecipeController
{
    [ApiController]
    public class SavedRecipeController : ControllerBase
    {
        private readonly ISavedRecipeService _savedRecipeService;

        public SavedRecipeController(ISavedRecipeService savedRecipeService)
        {
            _savedRecipeService = savedRecipeService;
        }

        [HttpGet("/savedrecipes")]
        public async Task<ActionResult> GetSavedRecipes(int userId)
        {
            try
            {
                var savedRecipes = await _savedRecipeService.GetUserSavedRecipesAsync(userId);
                return Ok(savedRecipes);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

                return NotFound(ErrorMessages.NotFoundMessage);
            }
        }

        [HttpPost("/savedrecipe/{userId}")]
        public async Task<ActionResult> SaveRecipe(int userId, [FromBody] SavedRecipeDto request)
        {
            try
            {
                var savedRecipe = await _savedRecipeService.SaveRecipeAsync(userId, request.RecipeId);

                return CreatedAtAction(nameof(GetSavedRecipes), new { userId }, savedRecipe);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

                return BadRequest(ErrorMessages.BadRequestMessage);
            }
        }

        [HttpDelete("/savedrecipe/{userId}/{recipeId}")]
        public async Task<ActionResult> DeleteSavedRecipe(int userId, int recipeId)
        {
            try
            {
                await _savedRecipeService.DeleteSavedRecipeAsync(userId, recipeId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

                return NotFound(ErrorMessages.NotFoundMessage);
            }
        }

    }
}
