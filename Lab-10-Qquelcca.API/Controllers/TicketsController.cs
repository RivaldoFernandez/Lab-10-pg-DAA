using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Application.DTOs.Ticket;
using Lab_10_Qquelcca.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab_10_Qquelcca.Controllers;

[ApiController]
[Route("api/soporte")] // Ruta base en español
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    /// <summary>
    /// Crea un nuevo ticket de soporte.
    /// </summary>
    [HttpPost("crear")]
    public async Task<IActionResult> Crear([FromBody] CreateTicketRequest request)
    {
        var id = await _ticketService.CreateTicketAsync(request);
        return Ok(new { ticketId = id });
    }

    /// <summary>
    /// Asigna un técnico a un ticket existente.
    /// </summary>
    [HttpPost("asignar/{ticketId}")]
    public async Task<IActionResult> Asignar(Guid ticketId, [FromQuery] Guid technicianId)
    {
        var ok = await _ticketService.AssignTicketAsync(ticketId, technicianId);
        return ok ? Ok("Ticket asignado") : BadRequest("Error al asignar");
    }

    /// <summary>
    /// Agrega una respuesta a un ticket.
    /// </summary>
    [HttpPost("responder")]
    public async Task<IActionResult> Responder([FromBody] AddResponseRequest request)
    {
        var ok = await _ticketService.AddResponseAsync(request);
        return ok ? Ok("Respuesta añadida") : BadRequest("Error al responder");
    }

    /// <summary>
    /// Cierra un ticket existente.
    /// </summary>
    [HttpPost("cerrar/{ticketId}")]
    public async Task<IActionResult> Cerrar(Guid ticketId)
    {
        var ok = await _ticketService.CloseTicketAsync(ticketId);
        return ok ? Ok("Ticket cerrado") : BadRequest("Error al cerrar");
    }
}