using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.CategoryController;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{

    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var categoryDTOs = await _categoryService.GetAllAsync();
            return Ok(categoryDTOs);
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int categoryId)
    {
        try
        {
            var categoryDTO = await _categoryService.GetByIdAsync(categoryId);
            return Ok(categoryDTO);

        }
        catch (Exception ex)
        {
            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpDelete("category/{categoryId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int categoryId)
    {
        try
        {
            await _categoryService.DeleteByIdAsync(categoryId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ErrorMessages.NotFoundMessage);
        }

    }



    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            await _categoryService.AddAsync(categoryDTO);
            return Ok($"Category successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }


}