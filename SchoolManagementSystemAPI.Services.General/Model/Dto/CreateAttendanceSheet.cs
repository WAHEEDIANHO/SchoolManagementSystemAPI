namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class CreateAttendanceSheet
    {
        public int GradeNumber { get; set; }
        public string SessionName { get; set; } = String.Empty;
        public int Term { get; set; } = 0;
        public int TotalExpectedAttendance { get; set; } = 0;

    }
}
