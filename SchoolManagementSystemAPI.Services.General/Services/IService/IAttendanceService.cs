using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface IAttendanceService
    {
        Task<bool> CreateGradeAttendanceSheet(CreateAttendanceSheet createAttendanceSheet);
        Task<bool> MarkAttendant(AttendanceDetailReqDTO attendance);
        Task<bool> UnmarkAttendant(string attendanceID);
        Task<bool> UpdateAttendant(string attendanceId, string timeOut);

        //Task<AttendanceDTO> GetGradeTermAttendant();
        Task<IEnumerable<AttendanceHeaderDTO>> GetAttendanceSheet(Dictionary<string, string>? filter = null);
        Task<IEnumerable<AttendanceDetailDTO>> GetStudentAttendant(string attendanceSheetId, string userId);

    }
}
