using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Iterfaces;
using RestaurantOrderAPI.Services;

namespace RestaurantOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null)
            return Unauthorized("Email ou senha inválidos");

        return Ok(result);
    }
}