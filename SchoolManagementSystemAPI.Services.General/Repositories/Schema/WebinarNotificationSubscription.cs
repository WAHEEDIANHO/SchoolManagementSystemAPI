using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

public class WebinarNotificationSubscription
{
    [Key]
    public string WebinarNotificationSubscriptionId { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string WebinarId { get; set; } = string.Empty;
    
    [ForeignKey("WebinarId")]
    public Webinar? Webinar { get; set; }
}