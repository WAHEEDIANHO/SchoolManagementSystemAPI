using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;
[PrimaryKey("TopicId", "WebinarDate", "WebinarHour")]
public class Webinar
{
    [Key]
    public string WebinarId { get; set; } = Guid.NewGuid().ToString();
    public string TeacherInCharge { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string TopicId { get; set; } = string.Empty;
    public DateTime WebinarDate { get; set; }
    public string WebinarHour { get; set; } = string.Empty;
    public string WebinarMinute { get; set; } = string.Empty;

    [ForeignKey("TopicId")]
    public Topic Topic { get; set; }
}