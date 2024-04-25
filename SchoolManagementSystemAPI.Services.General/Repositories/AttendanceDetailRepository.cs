using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class AttendanceDetailRepository :GenericRepository<AttendanceDetail, AppDbContext>, IAttendanceDetailRepository
    {
        private readonly AppDbContext _context;

        public AttendanceDetailRepository(AppDbContext context): base(context) { _context = context; }

        public IEnumerable<AttendanceDetail> GetStudentAttendant(string attendanceSheetId, string userId)
        {
            /*if(!Guid.TryParse(attendanceSheetId, out var parseAttendanceSheetId)) 
            {
                throw new Exception("Invalid Attendance sheet Id");
            }*/
            var res = _context.Set<AttendanceHeader>().Include(x => x.AttendanceDetails)
                .First(u => u.AttendanceHeaderId == attendanceSheetId);
                return res.AttendanceDetails.Where(u => u.UserId == userId).AsEnumerable();
        }
    }
}
