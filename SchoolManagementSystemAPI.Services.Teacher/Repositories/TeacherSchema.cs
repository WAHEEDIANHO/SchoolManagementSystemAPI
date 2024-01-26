using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.Teacher.Repositories
{
    public class TeacherSchema
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public string CourseOfStudy { get; set; }
        [Required]
        public string LevelOfStudy { get; set; }
        [Key]
        public string RegId { get; set; }
    }
}
