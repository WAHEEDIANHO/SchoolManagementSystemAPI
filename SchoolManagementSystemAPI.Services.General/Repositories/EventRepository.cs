using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class EventRepository: GenericRepository<Event, AppDbContext>, IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Event> GetByKey(string id)
        {
            return await _context.Set<Event>()
                .AsNoTracking()
                .FirstAsync(x => x.EventId.ToLower() == id.ToLower());
        }
    }
}
