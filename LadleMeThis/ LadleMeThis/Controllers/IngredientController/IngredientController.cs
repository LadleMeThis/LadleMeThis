using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.IngredientRepository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.IngredientController;
[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private IIngredientRepository _ingredientRepository;

    public IngredientController(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            return Ok(ingredients);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([Required] int id)
    {
        try
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            return Ok(ingredient);

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
            await _ingredientRepository.DeleteByIdAsync(ingredientId);
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
            await _ingredientRepository.AddAsync(new Ingredient() { Name = ingredientDTO.Name, Unit = ingredientDTO.Unit });
            return Ok($"Ingredient successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}