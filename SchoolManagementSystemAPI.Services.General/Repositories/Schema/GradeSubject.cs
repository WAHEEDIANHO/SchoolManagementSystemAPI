using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    [PrimaryKey("GradeNumber", "SubjectTitle")]
    public class GradeSubject
    {
        
        //public string GradeSubjectId { get; } = Guid.NewGuid().ToString();
        public int GradeNumber { get; set; }
        public string SubjectTitle { get; set; } = string.Empty;
        [ForeignKey("GradeNumber")]
        public Grade Grade { get; set; }
        [ForeignKey("SubjectTitle")]
        public Subject Subject { get; set; }
        public string? SubjectTeacher { get; set; }

        public ICollection<Topic>? Topics { get; set; }
        public ICollection<Discussion>? Discussions { get; set; }
    }
}
