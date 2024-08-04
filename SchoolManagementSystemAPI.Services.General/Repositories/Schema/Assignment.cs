using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class Assignment: Common
{
    [Key]
    public string AssignmentId { get; set; } = Guid.NewGuid().ToString();
    // [Required] 
    // public string LessonId { get; set; }
    [Required]
    public string AssignmentContent { get; set; }
    [Required] 
    public DateTime Deadline { get; set; }

    [Required] 
    public int DeadlineHour { get; set; }
    [Required] 
    public int DeadlineMinute { get; set; }

    // [Required]
    // public int GradeSubjectGradeNumber { get; set; }
    // [Required]
    // public string GradeSubjectSubjectTitle { get; set; } 
    
    // // Relationship
    // public GradeSubject GradeSubject { get; set; }
    // [ForeignKey("LessonId")]
    // public Lesson Lesson { get; set; }
}