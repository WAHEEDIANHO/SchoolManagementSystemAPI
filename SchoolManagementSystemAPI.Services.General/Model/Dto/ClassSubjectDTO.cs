using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class ClassSubjectDTO
    {
        public int GradeNumber { get; set; }
        public string SubjectTitle { get; set; }
        public string? SubjectTeacher { get; set; }
        
        public ICollection<TopicDTO>? Topics { get; set; }
    }
}
