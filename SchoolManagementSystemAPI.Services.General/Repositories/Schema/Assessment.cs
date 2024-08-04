using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class Assessment: Common
{
    [Key]
    public string AssessmentId { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Title { get; set; } 
    public string? Description  { get; set; } 
    // [Required] 
    // public string LessonId { get; set; }
    [Required]
    public string AssessmentContent { get; set; }
    [Required] 
    public int Duration { get; set; }
    [Required] 
    public DateTime DateSchedule { get; set; }
    [Required] 
    public int HourSchedule { get; set; }
    [Required] 
    public int MinuteSchedule { get; set; }

}