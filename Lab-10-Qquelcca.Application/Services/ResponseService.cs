using Lab_10_Qquelcca.Application.DTOs;
using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;

namespace Lab_10_Qquelcca.Application.Services;

public class ResponseService
{
    private readonly IResponseRepository _repository;

    public ResponseService(IResponseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Response>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Response?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Response> CreateAsync(CreateResponseDto dto)
    {
        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = dto.TicketId,
            ResponderId = dto.ResponderId,
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(response);
        await _repository.SaveChangesAsync();
        return response;
    }
}