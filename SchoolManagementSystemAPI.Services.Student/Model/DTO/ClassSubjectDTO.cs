using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.Student.Model.Dto
{
    public class ClassSubjectDTO
    {
        public int GradeNumber { get; set; }
        public string SubjectTitle { get; set; }
        public string? SubjectTeacher { get; set; }
        
        public ICollection<TopicDTO>? Topics { get; set; }
    }
}
