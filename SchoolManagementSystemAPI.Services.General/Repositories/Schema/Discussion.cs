using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class Discussion
{
    [Key]
    public string DisscusionId { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string StudentId { get; set; }
    [Required]
    public string Message { get; set; }
    public string? DiscussionDisscusionId { get; set; }
    [Required] 
    public string LessonId { get; set; }
    [Required] 
    public DateTime DissusionDateTime { get; set; } = DateTime.Now;
    [Required]
    public int GradeSubjectGradeNumber { get; set; }
    [Required]
    public string GradeSubjectSubjectTitle { get; set; } 
    
    // Relationship
    public GradeSubject GradeSubject { get; set; }
    public ICollection<Discussion>? DiscussionReplies { get; set; }
    [ForeignKey("LessonId")]
    public Lesson Lesson { get; set; }
    
}