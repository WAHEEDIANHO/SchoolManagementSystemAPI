namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class DiscussionReqDto
{
    public string Message { get; set; }
    public string? DiscussionDisscusionId { get; set; }
    public string LessonId { get; set; }
    public DateTime DissusionDateTime { get; set; } = DateTime.Now;
    public int GradeSubjectGradeNumber { get; set; }
    public string GradeSubjectSubjectTitle { get; set; } 
    
}