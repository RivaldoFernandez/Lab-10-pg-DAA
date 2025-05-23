using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.Infrastructure.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _context;

        public ResponseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> GetByIdAsync(Guid id)
        {
            return await _context.Responses.FindAsync(id);
        }

        public async Task<IEnumerable<Response>> GetAllAsync()
        {
            return await _context.Responses.ToListAsync();
        }

        public async Task AddAsync(Response response)
        {
            await _context.Responses.AddAsync(response);
        }

        public void Update(Response response)
        {
            _context.Responses.Update(response);
        }

        public void Remove(Response response)
        {
            _context.Responses.Remove(response);
        }
    }
}