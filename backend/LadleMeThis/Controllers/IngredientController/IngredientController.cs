using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Services.IngredientService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.IngredientController;
[ApiController]
[Route("ingredients")]
public class IngredientController : ControllerBase
{
    private IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var ingredientDTOs = await _ingredientService.GetAllAsync();
            return Ok(ingredientDTOs);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpGet("/ingredient/{ingredientId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int ingredientId)
    {
        try
        {
            var ingredientDTO = await _ingredientService.GetByIdAsync(ingredientId);
            return Ok(ingredientDTO);

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpDelete("/ingredient/{ingredientId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int ingredientId)
    {
        try
        {
            await _ingredientService.DeleteByIdAsync(ingredientId);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] IngredientCreateRequest ingredientCreateRequest)
    {
        try
        {
            var ingredient = await _ingredientService.AddAsync(ingredientCreateRequest);
            return Ok(ingredient);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }



}