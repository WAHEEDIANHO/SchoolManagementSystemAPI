using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class SessionRepository: GenericRepository<Session, AppDbContext>, IRepositories.ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Session> GetById(string SessionName)
        {
            return await _context.Set<Session>().FirstAsync(x => x.SessionName == SessionName);
        }
    }
}
