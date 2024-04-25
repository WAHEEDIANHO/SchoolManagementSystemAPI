namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class WebinarReqDto
{
    public string TeacherInCharge { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string TopicId { get; set; } = string.Empty;
    public DateTime WebinarDate { get; set; }
    public string WebinarHour { get; set; } = string.Empty;
    public string WebinarMinute { get; set; } = string.Empty;
}