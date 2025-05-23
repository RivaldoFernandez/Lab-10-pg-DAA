using Lab_10_Qquelcca.Application.DTOs.Role;
using Lab_10_Qquelcca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var result = await _roleService.CreateRoleAsync(request);

            if (!result)
                return BadRequest("El rol ya existe");

            return Ok("Rol creado correctamente");
        }
    }
}