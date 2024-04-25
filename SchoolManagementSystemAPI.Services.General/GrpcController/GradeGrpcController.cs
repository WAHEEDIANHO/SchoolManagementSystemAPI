

using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcGrade;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class GradeGrpcController : GrpcGradeBase
    {
        private readonly IMapper _mapper;
        private readonly IGradeService _service;

        public GradeGrpcController(IMapper mapper, IGradeService service) 
        {
            _mapper = mapper;
            _service = service;
        }
        public override Task<GradeActionState> ChangeGradeTeacher(GradeDataGrpc request, ServerCallContext context)
        {
           GradeActionState proState = new();
           proState.Resp = _service.updateClassTeacher(_mapper.Map<GradeDTO>(request)).GetAwaiter().GetResult();
           return Task.FromResult(proState);
        }

        public override Task<GradeActionState> CreateGrade(GradeDataGrpc request, ServerCallContext context)
        {
            GradeActionState prosState = new();
           prosState.Resp = _service.AddClass(_mapper.Map<GradeDTO>(request)).GetAwaiter().GetResult();
            return Task.FromResult(prosState);
        }

        public override Task<GradeActionState> DeleteGrade(ResourceId request, ServerCallContext context)
        {
            GradeActionState prosState = new();
            prosState.Resp = _service.deleteClassbyId(int.Parse(request.Id)).GetAwaiter().GetResult();  
            return Task.FromResult(prosState);
        }

        public override Task<ListGrade> GetAllGrade(GradeEmpty request, ServerCallContext context)
        {
            ListGrade resp = new();
            var grades =_service.getAllClass().GetAwaiter().GetResult();  
            foreach(var grade in grades) { resp.Grades.Add(_mapper.Map<GradeDataGrpc>(grade));}
            return Task.FromResult(resp);
        }

        public override Task<GradeDataGrpc> GetByGradeId(ResourceId request, ServerCallContext context)
        {
            var res = _service.getClassByKey(int.Parse(request.Id)).GetAwaiter().GetResult();
            return Task.FromResult(_mapper.Map<GradeDataGrpc>(res));
        }
    }
}
