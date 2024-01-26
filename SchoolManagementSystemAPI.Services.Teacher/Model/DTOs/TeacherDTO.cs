using SchoolManagementSystemAPI.Services.Teacher.Repositories;

namespace SchoolManagementSystemAPI.Services.Teacher.Model.DTOs
{
    public class TeacherDTO: UserResponseDTO
    {
        public TeacherDTO(UserResponseDTO user, TeacherSchema teacher): base(user) {
        
            AppointmentDate = teacher.AppointmentDate;
            Grade = teacher.Grade;
            CourseOfStudy = teacher.CourseOfStudy;
            LevelOfStudy = teacher.LevelOfStudy;
            RegId = teacher.RegId;
        }
        public TeacherDTO() { }
        public DateTime AppointmentDate { get; set; }
        public int Grade { get; set; }
        public string CourseOfStudy { get; set; }
        public string LevelOfStudy { get; set; }
        public string RegId { get; set; }
    }
}
