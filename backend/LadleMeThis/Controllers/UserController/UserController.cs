using System.Security.Claims;
using LadleMeThis.Models.AuthContracts;
using LadleMeThis.Models.ErrorMessages;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Services.UserService;
using Microsoft.AspNetCore.Authorization;
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
	
	[Authorize]
	[HttpGet("/user")]
	public async Task<ActionResult> GetUserById()
	{
		try
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = await _userService.GetUserByIdAsync(userId);
			return Ok(user);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);

			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}
	
	[Authorize]
	[HttpPut("/user")]
	public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDto)
	{
		try
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			await _userService.UpdateUserAsync(userId, userUpdateDto);
			return NoContent();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);

			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}

	[Authorize]
	[HttpDelete("/user")]
	public async Task<ActionResult> DeleteUser()
	{
		try
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			await _userService.DeleteUserAsync(userId);
			return NoContent();
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);

			return NotFound(ErrorMessages.NotFoundMessage);
		}
	}

	[HttpPost("register")]
	public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
	{
		try
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			const string role = "User";
			var result = await _userService.RegisterAsync(registrationRequest, role);

			if (!result.Success)
			{
				AddErrors(result);
				return BadRequest(ModelState);
			}

			return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
		}
		catch (Exception ex)
		{
			return BadRequest(ErrorMessages.BadRequestMessage);
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);


		var result = await _userService.LoginAsync(request);

		var cookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddHours(1)
		};

		Response.Cookies.Append("AuthToken", result.Token, cookieOptions);

		if (result.Success)
			return Ok();

		AddErrors(result);
		return BadRequest(ModelState);
	}

	[HttpPost("logout")]
	public IActionResult Logout()
	{
		Response.Cookies.Delete("AuthToken");
		return Ok("Logged out successfully");
	}

	private void AddErrors(AuthResult result)
	{
		foreach (var error in result.ErrorMessages)
			ModelState.AddModelError(error.Key, error.Value);
	}
}