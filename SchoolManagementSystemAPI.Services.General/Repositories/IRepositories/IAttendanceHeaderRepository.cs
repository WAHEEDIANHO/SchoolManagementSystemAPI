using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface IAttendanceHeaderRepository: IGenericRepository<AttendanceHeader, AppDbContext>
    {
        Task<AttendanceHeader> GetSpecifyAttendanceSheet(CreateAttendanceSheet createAttendanceHeaderReq);
    }
}
