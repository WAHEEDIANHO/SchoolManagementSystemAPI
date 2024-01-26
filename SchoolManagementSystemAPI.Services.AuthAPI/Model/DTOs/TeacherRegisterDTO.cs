namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class TeacherRegisterDTO: RegisterRequestDTO
    {
        public DateTime AppointmentDate { get; set; }
        public int Grade { get; set; }
        public string CourseOfStudy { get; set; }
        public string LevelOfStudy { get; set; }

    }
}
