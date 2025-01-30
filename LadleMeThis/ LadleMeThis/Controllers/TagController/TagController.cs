using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.TagRepository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.TagController;
[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    ITagRepository _tagRepository;
    public TagController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var tags = await _tagRepository.GetAllAsync();
            return Ok(tags);
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
            var tag = await _tagRepository.GetByIdAsync(tagId);
            return Ok(tag);

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
            await _tagRepository.DeleteByIdAsync(tagId);
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
            await _tagRepository.AddAsync(new Tag() { Name = tagDTO.Name, });
            return Ok($"Tag successfully created!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}