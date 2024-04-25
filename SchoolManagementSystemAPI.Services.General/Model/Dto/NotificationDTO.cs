using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class NotificationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string PostedBy { get; set; } = string.Empty;
    }
}
