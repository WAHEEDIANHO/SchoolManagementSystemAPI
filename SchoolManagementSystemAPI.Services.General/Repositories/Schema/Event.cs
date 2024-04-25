using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    [PrimaryKey("EventId")]
    public class Event
    {
        [Key] 
        public string EventId { get; } = Guid.NewGuid().ToString();
        [Required]
        public int Term { get; set; } 
        [Required]
        public DateTime EventDate { get; set; }
        [Required] 
        public string EventHour { get; set; } = string.Empty;
        [Required] 
        public string EventMinute { get; set; } = string.Empty;
        [Required] 
        public string EventTitle { get; set; } = string.Empty;
        [Required]
        public string SessionName { get; set; }

        public string? Description { get; set; }
        
        [ForeignKey("SessionName")]
        public Session Session { get; set; }
    }
}
