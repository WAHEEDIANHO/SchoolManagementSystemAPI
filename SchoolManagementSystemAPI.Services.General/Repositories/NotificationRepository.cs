using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class NotificationRepository: GenericRepository<Notification, AppDbContext>, INotificationRepository 
    {
        public NotificationRepository(AppDbContext context): base(context) { }
    }
}
