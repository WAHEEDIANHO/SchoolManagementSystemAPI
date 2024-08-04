using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class TermDto
{
    public string SessionName { get; set; }
    public string TermName { get; set; }
    public DateTime TermStartDate { get; set; }
    public DateTime TermEndDate { get; set; }
    public int TermNumber { get; set; }
    public TermStatus TermStatus { get; set; } // "ongoing", "completed"
    
    // public Session Session { get; set; }
}