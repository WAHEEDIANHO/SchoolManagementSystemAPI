using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface IAttendanceService
    {
        Task<bool> CreateGradeAttendanceSheet(AttendanceHeaderReqDTO attendanceSheet);
        Task<bool> MarkAttendant(AttendanceDetailReqDTO attendance);
        Task<bool> UnmarkAttendant(string attendanceID);
        Task<bool> UpdateAttendant(string attendanceId, string timeOut);

        //Task<AttendanceDTO> GetGradeTermAttendant();
        Task<IEnumerable<AttendanceHeaderDTO>> GetAttendanceSheet();
        Task<IEnumerable<AttendanceDetailDTO>> GetStudentAttendant(string attendanceSheetId, string userId);

    }
}
