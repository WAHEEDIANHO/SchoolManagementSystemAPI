using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcSession;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class SessionGrpcController : GrpcSessionBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _service;

        public SessionGrpcController(IMapper mapper, ISessionService service) 
        {
            _mapper = mapper; 
            _service = service;
        }
        public override Task<SessionActionState> CreateSession(SessionGrpcData request, ServerCallContext context)
        { 
            SessionActionState resp = new();
            resp.Resp = _service.AddClass(_mapper.Map<SessionDTO>(request)).GetAwaiter().GetResult();
            return Task.FromResult(resp);
        }

        public override Task<SessionGrpcData> DeleteSessionById(SessionId request, ServerCallContext context)
        {
            SessionGrpcData resp = new();
            resp = _mapper.Map< SessionGrpcData>(_service.deleteSessionbyId(request.Id).GetAwaiter().GetResult());  
            return Task.FromResult(resp);   
        }

        public override Task<ListSession> GetAllSession(GetEmptySession request, ServerCallContext context)
        {
            ListSession resp = new();
            var sessions = _service.getAllClass().GetAwaiter().GetResult();
            foreach ( var session in sessions ) { resp.Sessions.Add(_mapper.Map<SessionGrpcData>(session)); }
            return Task.FromResult(resp);
        }

        public override Task<SessionGrpcData> GetSessionById(SessionId request, ServerCallContext context)
        {
            var resp = _mapper.Map<SessionGrpcData>(_service.getSessionById(request.Id));
            return Task.FromResult(resp);
        }
    }
}
