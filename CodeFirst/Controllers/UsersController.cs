using CodeFirst.DTO;
using CodeFirst.Middleware;
using CodeFirst.Models;
using CodeFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public IActionResult RegisterUser(NewUserRequest request)
    {
        _userService.RegisterUser(request);
        return Ok();
    }

    [HttpPost("api/login")]
    public IActionResult LoginUser(LoginRequest request)
    {
        var token = _userService.GenerateJwt(request);
        return Ok(token);
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        try
        {
            var newToken = _userService.RefreshToken(request.RefreshToken);

            return Ok(new { token = newToken });
        }
        catch (UnauthorizedException)
        {
            return Unauthorized();
        }
    }

}