using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class Common
{
    [Required] 
    public string LessonId { get; set; }
    [Required]
    public int GradeSubjectGradeNumber { get; set; }
    [Required]
    public string GradeSubjectSubjectTitle { get; set; } 
    
    // Relationship
    public GradeSubject GradeSubject { get; set; }
    [ForeignKey("LessonId")]
    public Lesson Lesson { get; set; }
}