using System.Runtime.InteropServices.JavaScript;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Services;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace LadleMeThis.Controllers.UserController;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/users")]
    public async Task<ActionResult> GetAllUser()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);

            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }

    [HttpGet("/user/{userId}")]
    public async Task<ActionResult> GetUserById(int userId)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);

            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }

    [HttpPost("/users")]
    public async Task<ActionResult> CreateUser([FromBody] UserDTO userDto)
    {
        try
        {
            await _userService.CreateUserAsync(userDto);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);

            return BadRequest(ErrorMessages.BadRequestMessage);
        }
    }

    [HttpPut("/user/{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserDTO userDto)
    {
        try
        {
            await _userService.UpdateUserAsync(userId, userDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);

            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }

    [HttpDelete("/user/{userId}")]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);

            return NotFound(ErrorMessages.NotFoundMessage);
        }
    }






   

}