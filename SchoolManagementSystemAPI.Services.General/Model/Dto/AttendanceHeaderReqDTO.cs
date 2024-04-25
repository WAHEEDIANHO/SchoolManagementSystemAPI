namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class AttendanceHeaderReqDTO
    {
        public int GradeNumber { get; set; }
        public string SessionName { get; set; } = String.Empty;
        public int Term { get; set; } = 0;
        public int totalExpectedAttendance { get; set; } = 0;

    }
}
