using Lab_10_Qquelcca.Application.DTOs.UserRole;

namespace Lab_10_Qquelcca.Controllers;

using Lab_10_Qquelcca.Application.DTOs;
using Lab_10_Qquelcca.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class UserRolesController : ControllerBase
{
    private readonly UserRoleService _userRoleService;

    public UserRolesController(UserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] UserRoleAssignDto dto)
    {
        try
        {
            await _userRoleService.AssignRoleAsync(dto);
            return Ok("Rol asignado correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

