using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class WebinarNotificationSubscriptionRepository: GenericRepository<WebinarNotificationSubscription, AppDbContext>, IWebinarNotificationSubscriptionRepository
{
    public WebinarNotificationSubscriptionRepository(AppDbContext dbContext) : base(dbContext) {}
}