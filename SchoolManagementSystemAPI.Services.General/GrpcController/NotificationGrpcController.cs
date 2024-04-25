using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcNotification;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class NotificationGrpcController : GrpcNotificationBase
    {
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public NotificationGrpcController(IMapper mapper, INotificationService service) 
        {
            _service = service;
            _mapper = mapper;
        }
        public override Task<NotificationActionState> CreateNotification(NotificationGrpcReqData request, ServerCallContext context)
        {
            NotificationActionState notificationActionState = new NotificationActionState();
            _service.CreateNotification(_mapper.Map<NotificationDTO>(request)).GetAwaiter().GetResult();
            notificationActionState.Res = true;
            return Task.FromResult(notificationActionState);
        }

        public override Task<ListNotification> GetAllNotifications(GetEmptyNotification request, ServerCallContext context)
        {
            ListNotification resp = new ListNotification();
            var notifications = _service.GetAllNotifications().GetAwaiter().GetResult();
            foreach ( var notification in notifications ) { resp.Notifications.Add(_mapper.Map<NotificationGrpcData>(notification)); }
            return Task.FromResult(resp); 
        }

        public override Task<NotificationGrpcData> GetNotification(NotificationId request, ServerCallContext context)
        {
            var notification = _service.GetNotification(request.Id).GetAwaiter().GetResult();
            return Task.FromResult(_mapper.Map<NotificationGrpcData>(notification)); 
        }

        public override Task<NotificationActionState> RemoveNotification(NotificationId request, ServerCallContext context)
        {
            NotificationActionState actionState = new NotificationActionState();
           actionState.Res = _service.RemoveNotification(request.Id).GetAwaiter().GetResult();
            return Task.FromResult(actionState);
        }

        public override Task<NotificationActionState> UpdateNotificatiion(NotificationUpdate request, ServerCallContext context)
        {
            NotificationActionState actionState = new NotificationActionState();
            actionState.Res = _service
                .UpdateNotificatiion(request.NotificationId, _mapper.Map<NotificationDTO>(request.NotificationUpdateData))
                .GetAwaiter().GetResult(); 
            return Task.FromResult(actionState);
        }
    }
}
