using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcSubject;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class SubjectGrpcController : GrpcSubjectBase
    {
        private readonly IMapper _mapper;
        private readonly ISubjectServices _service;

        public SubjectGrpcController(IMapper mapper, ISubjectServices service) 
        {
            _mapper = mapper;
            _service = service;
        }
        public override Task<SubjectActionState> CreateSubject(SubjectDataGrpc request, ServerCallContext context)
        {
            SubjectActionState state = new();
            state.Res = _service.CreateSubject(_mapper.Map<SubjectRequestDTO>(request)).GetAwaiter().GetResult();
            return Task.FromResult(state);
        }

        public override Task<SubjectDataGrpc> DeleteSubjectbyId(ById request, ServerCallContext context)
        {
            var sub = _service.DeleteSubjectByID(request.Id).GetAwaiter().GetResult();
            return Task.FromResult(_mapper.Map<SubjectDataGrpc>(sub));
        }

        public override Task<ListSubject> GetAllSubject(GetAll request, ServerCallContext context)
        {
            ListSubject res = new();
            var subjects = _service.GetAllSubjects().GetAwaiter().GetResult();
            foreach (var subject in subjects) { res.Subjects.Add(_mapper.Map<SubjectDataGrpc>(subject)); }
            return Task.FromResult(res);
        }

        public override Task<SubjectDataGrpc> GetSubjectbyId(ById request, ServerCallContext context)
        {
            var subject = _service.GetSubjectByID(request.Id).GetAwaiter().GetResult();
            return Task.FromResult(_mapper.Map<SubjectDataGrpc>(subject));
        }
    }
}
