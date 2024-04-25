using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    public class Notification
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string PostedBy { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
