using LadleMeThis.Models.DTO.SavedRecipe;
using LadleMeThis.Services.SavedRecipeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.SavedRecipeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedRecipeController : ControllerBase
    {
        private readonly ISavedRecipeService _savedRecipeService;

        public SavedRecipeController(ISavedRecipeService savedRecipeService)
        {
            _savedRecipeService = savedRecipeService;
        }

        [HttpGet("{userId}/saved-recipes")]
        public async Task<ActionResult> GetSavedRecipes(int userId)
        {
            try
            {
                var savedRecipes = await _savedRecipeService.GetUserSavedRecipesAsync(userId);
                return Ok(savedRecipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An internal error occurred.", Error = ex.Message });
            }
        }

        [HttpPost("{userId}/saved-recipes")]
        public async Task<ActionResult> SaveRecipe(int userId, [FromBody] SavedRecipeDto request)
        {
            try
            {
                var savedRecipe = await _savedRecipeService.SaveRecipeAsync(userId, request.RecipeId);

                return CreatedAtAction(nameof(GetSavedRecipes), new { userId }, savedRecipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An internal error occurred.", Error = ex.Message });
            }
        }

        [HttpDelete("{userId}/saved-recipes/{recipeId}")]
        public async Task<ActionResult> DeleteSavedRecipe(int userId, int recipeId)
        {
            try
            {
                var deleted = await _savedRecipeService.DeleteSavedRecipeAsync(userId, recipeId);
                if (!deleted)
                    return NotFound("Saved recipe not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An internal error occurred.", Error = ex.Message });
            }
        }

    }
}
