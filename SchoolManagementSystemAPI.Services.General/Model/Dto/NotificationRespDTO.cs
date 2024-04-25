namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class NotificationRespDTO
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string PostedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
