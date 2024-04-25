namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class AttendanceDetailReqDTO
    {
        public string AttendanceHeaderId { get; set; } = string.Empty;
        public string? AttendanceTimeOut { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}
