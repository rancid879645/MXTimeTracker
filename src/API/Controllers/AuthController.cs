using Microsoft.AspNetCore.Mvc;
using MotocrossTracker.API.Application.DTOs;
using MotocrossTracker.API.Application.Interfaces;

namespace MotocrossTracker.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var response = await userService.RegisterAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await userService.LoginAsync(request);

        if (response is null)
            return Unauthorized(new { error = "Invalid username or password." });

        return Ok(response);
    }
}
