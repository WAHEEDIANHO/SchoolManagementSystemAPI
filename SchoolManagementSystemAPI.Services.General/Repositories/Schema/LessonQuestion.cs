using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class LessonQuestion: Common
{
    [Key] 
    public string LessonQuestionId { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string StudentId { get; set; }
    [Required] 
    public string Question { get; set; }

    public string? Response { get; set; }
}