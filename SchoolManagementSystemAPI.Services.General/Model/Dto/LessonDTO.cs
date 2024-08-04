namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class LessonDTO
{
    public string LessonId { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Objectives { get; set; } = string.Empty;
    public string Transcript { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    // public IFormFile? Media { get; set; }
    public string TopicId { get; set; } = string.Empty;
}