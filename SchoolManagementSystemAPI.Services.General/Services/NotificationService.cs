using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> CreateNotification(NotificationDTO schSession)
        {
           Notification notification = _mapper.Map<Notification>(schSession);  
           await _repository.Add(notification);
            return true;
        }

        public async Task<IEnumerable<NotificationRespDTO>> GetAllNotifications(Dictionary<string, string>? filter = null)
        {
            var res = await _repository.GetAll(filter);
            return _mapper.Map<IEnumerable<NotificationRespDTO>>(res);
        }

        public async Task<NotificationRespDTO> GetNotification(string id)
        {
            var res = await _repository.GetByKey(new Guid(id));
            return _mapper.Map<NotificationRespDTO>(res);
        }

        public async Task<bool> RemoveNotification(string id)
        {
            var res = await _repository.GetByKey(new Guid(id));
            if(res != null)
            {
                 _repository.Delete(res);
                return true;
            }else { return false; }
        }

        public async Task<bool> UpdateNotificatiion(string id, NotificationDTO notification)
        {
            if(await _repository.GetByKey(id) != null)
            {
                Notification upNotification = _mapper.Map<Notification>(notification);
                upNotification.Id = id;
                _repository.Update(upNotification);
                return true;
            }else return false;
            
        }
    }
}
