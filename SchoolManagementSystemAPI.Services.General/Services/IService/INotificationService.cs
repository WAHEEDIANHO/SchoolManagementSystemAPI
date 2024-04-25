using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationRespDTO>> GetAllNotifications();
        Task<NotificationRespDTO> GetNotification(string id);
        Task<bool> RemoveNotification(string id);
        Task<bool> CreateNotification(NotificationDTO schSession);
        Task<bool> UpdateNotificatiion(string Id, NotificationDTO schSession);
    }
}
