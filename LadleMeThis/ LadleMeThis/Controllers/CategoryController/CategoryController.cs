using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.CategoryController;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    private ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int categoryId)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return Ok(category);

        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int categoryId)
    {
        try
        {
            await _categoryRepository.DeleteByIdAsync(categoryId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] CategoryDTO categoryDTO)
    {
        try
        {
            await _categoryRepository.AddAsync(new Category() { Name = categoryDTO.Name, });
            return Ok($"Category successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}