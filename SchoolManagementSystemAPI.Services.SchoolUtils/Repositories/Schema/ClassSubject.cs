using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema
{
    [PrimaryKey("GradeNumber", "SubjectTitle")]
    public class ClassSubject
    {
        [Key]
        [ForeignKey("Grade")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeNumber { get; set; }
        [Key]
        [ForeignKey("Subject")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectTitle { get; set; }
        public string? SubjectTeacher { get; set; }

    }
}
