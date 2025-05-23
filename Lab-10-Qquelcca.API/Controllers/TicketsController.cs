using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Application.DTOs.Ticket;
using Lab_10_Qquelcca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab_10_Qquelcca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        var id = await _ticketService.CreateTicketAsync(request);
        return Ok(new { ticketId = id });
    }

    [HttpPost("assign/{ticketId}")]
    public async Task<IActionResult> Assign(Guid ticketId, [FromQuery] Guid technicianId)
    {
        var ok = await _ticketService.AssignTicketAsync(ticketId, technicianId);
        return ok ? Ok("Ticket asignado") : BadRequest("Error al asignar");
    }

    [HttpPost("respond")]
    public async Task<IActionResult> Respond([FromBody] AddResponseRequest request)
    {
        var ok = await _ticketService.AddResponseAsync(request);
        return ok ? Ok("Respuesta añadida") : BadRequest("Error al responder");
    }

    [HttpPost("close/{ticketId}")]
    public async Task<IActionResult> Close(Guid ticketId)
    {
        var ok = await _ticketService.CloseTicketAsync(ticketId);
        return ok ? Ok("Ticket cerrado") : BadRequest("Error al cerrar");
    }
}
