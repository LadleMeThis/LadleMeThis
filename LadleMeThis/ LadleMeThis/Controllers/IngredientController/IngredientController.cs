using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Services.IngredientService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.IngredientController;
[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var ingredientDTOs = await _ingredientService.GetAllAsync();
            return Ok(ingredientDTOs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{ingredientId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int ingredientId)
    {
        try
        {
            var ingredientDTO = await _ingredientService.GetByIdAsync(ingredientId);
            return Ok(ingredientDTO);

        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{ingredientId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int ingredientId)
    {
        try
        {
            await _ingredientService.DeleteByIdAsync(ingredientId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] IngredientDTO ingredientDTO)
    {
        try
        {
            await _ingredientService.AddAsync(ingredientDTO);
            return Ok($"Ingredient successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}