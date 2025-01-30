using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.TagService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.TagController;
[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    ITagService _tagService;
    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var tagDTOs = await _tagService.GetAllAsync();
            return Ok(tagDTOs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{tagId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int tagId)
    {
        try
        {
            var tagDTO = await _tagService.GetByIdAsync(tagId);
            return Ok(tagDTO);

        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{tagId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int tagId)
    {
        try
        {
            await _tagService.DeleteByIdAsync(tagId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] TagDTO tagDTO)
    {
        try
        {
            await _tagService.AddAsync(tagDTO);
            return Ok($"Tag successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}