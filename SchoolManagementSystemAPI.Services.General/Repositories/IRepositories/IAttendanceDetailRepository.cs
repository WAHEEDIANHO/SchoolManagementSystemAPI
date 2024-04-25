using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface IAttendanceDetailRepository: IGenericRepository<AttendanceDetail, AppDbContext>
    {
        IEnumerable<AttendanceDetail> GetStudentAttendant(string attendanceId, string userId);
    }
}
    