namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class TopicDTO
{
    public string TopicId { get; set; } = new Guid().ToString();
    public string TopicName { get; set; } = string.Empty;
    public int Term { get; set; }
    public int GradeNumber { get; set; }
    public string SubjectTitle { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string TermSessionName { get; set; }
    public int TermTermNumber { get; set; }
        
    public ICollection<LessonDTO>? Lessons { get; set; }
        
}