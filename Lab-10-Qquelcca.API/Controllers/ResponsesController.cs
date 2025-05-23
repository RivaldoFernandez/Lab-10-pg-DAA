using Lab_10_Qquelcca.Application.DTOs;
using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab_10_Qquelcca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseController : ControllerBase
{
    private readonly ResponseService _service;

    public ResponseController(ResponseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _service.GetAllAsync();
        return Ok(responses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.ResponseId }, created);
    }
}