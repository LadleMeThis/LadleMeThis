using System.Runtime.InteropServices.JavaScript;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Services;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.UserController;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUser()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] UserDTO userDto)
    {
        try
        {
            await _userService.CreateUserAsync(userDto);
            return Ok(new { Message = "User created successfully" });
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
    {
        try
        {
            await _userService.UpdateUserAsync(id, userDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }






   

}