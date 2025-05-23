using Lab_10_Qquelcca.Application.DTOs.Auth;
using Lab_10_Qquelcca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result)
            return Ok(new { message = "User registered successfully" });

        return BadRequest("User registration failed");
    }
}