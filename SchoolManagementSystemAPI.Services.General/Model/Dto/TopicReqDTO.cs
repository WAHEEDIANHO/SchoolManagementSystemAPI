namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class TopicReqDTO
{
    public string TopicName { get; set; } = string.Empty;
    public int Term { get; set; }
    public int GradeNumber { get; set; }
    public string SubjectTitle { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}