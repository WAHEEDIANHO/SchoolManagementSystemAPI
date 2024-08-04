using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class AttendanceHeaderRepository : GenericRepository<AttendanceHeader, AppDbContext>, IAttendanceHeaderRepository
    {
        private readonly AppDbContext _context;

        public AttendanceHeaderRepository(AppDbContext context) : base(context) { _context = context; }

        public async Task<AttendanceHeader> GetSpecifyAttendanceSheet(CreateAttendanceSheet createAttendanceHeaderReq)
        {
           return await _context.Set<AttendanceHeader>().Include(x => x.AttendanceDetails)
                .FirstAsync(u => (u.SessionName == createAttendanceHeaderReq.SessionName && 
                u.GradeNumber == createAttendanceHeaderReq.GradeNumber &&
                u.Term == createAttendanceHeaderReq.Term) );
        }

        public async Task<IEnumerable<AttendanceHeader>> GetAll()
        {
            return await _context.Set<AttendanceHeader>().Include(x => x.AttendanceDetails).ToListAsync();
        }
    }
}
