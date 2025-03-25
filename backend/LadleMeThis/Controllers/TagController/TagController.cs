using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.TagService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Controllers.TagController;
[ApiController]
[Route("tags")]
public class TagController : ControllerBase
{
    ITagService _tagService;
    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var tagDTOs = await _tagService.GetAllAsync();
            return Ok(tagDTOs);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpGet("/tag/{tagId}")]
    public async Task<IActionResult> GetByIdAsync([Required] int tagId)
    {
        try
        {
            var tagDTO = await _tagService.GetByIdAsync(tagId);
            return Ok(tagDTO);

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpDelete("/tag/{tagId}")]
    public async Task<IActionResult> DeleteByIdAsync([Required] int tagId)
    {
        try
        {
            await _tagService.DeleteByIdAsync(tagId);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return NotFound(ErrorMessages.NotFoundMessage);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddAsnyc([FromBody] TagCreateRequest tagCreateRequest)
    {
        try
        {
            var tagDTO = await _tagService.AddAsync(tagCreateRequest);
            return Ok(tagDTO);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }

}