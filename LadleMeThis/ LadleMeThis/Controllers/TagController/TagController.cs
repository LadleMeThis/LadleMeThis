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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([Required] int id)
    {
        try
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return Ok(tag);

        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }


}