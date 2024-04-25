namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class EventDTO
{
    public string EventId { get; set; } = string.Empty;
    public string SessionName { get; set; } = string.Empty;
    public int Term { get; set; } 
    public DateTime EventDate { get; set; }
    public string EventHour { get; set; } = string.Empty;
    public string EventMinute { get; set; } = string.Empty;
    public string EventTitle { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class EventReqDTO
{
    public string SessionName { get; set; } = string.Empty;
    public int Term { get; set; } 
    public DateTime EventDate { get; set; }
    public string EventHour { get; set; } = string.Empty;
    public string EventMinute { get; set; } = string.Empty;
    public string EventTitle { get; set; } = string.Empty;
    public string? Description { get; set; }
}