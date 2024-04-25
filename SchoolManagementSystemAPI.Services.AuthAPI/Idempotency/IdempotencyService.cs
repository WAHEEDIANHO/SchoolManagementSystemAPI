using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Idempotency
{
    public sealed class IdempotencyService : IIdenpotencyServices
    {
        private readonly AppDbContext _context;

        public IdempotencyService(AppDbContext context) {
            _context = context;
        }
        public async Task CreateRequestAsync(Guid requestId, int statusCode, string name, ResponseDTO response)
        {
            IdempotencySchema idempotentRequest = new IdempotencySchema
            {
                Id = requestId,
                Name = name,
                CreatedOnUTC = DateTime.UtcNow,
                StatusCode = statusCode,
                Response = JsonConvert.SerializeObject(response)
            };

           await  _context.AddAsync(idempotentRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IdempotencySchema> GetRequestResponse(Guid requestId)
        {
            return await _context.Set<IdempotencySchema>().AsNoTracking().FirstAsync(u => u.Id == requestId);
        }

        public async Task<bool> RequestExistAsync(Guid requestId)
        {
            return await _context.Set<IdempotencySchema>().AnyAsync(u => u.Id == requestId);
        }
    }
}
