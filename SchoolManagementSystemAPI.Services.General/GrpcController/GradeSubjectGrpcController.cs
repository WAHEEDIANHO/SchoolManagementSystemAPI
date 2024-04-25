using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using static SchoolManagementSystemAPI.Services.General.GrpcGradeSubject;

namespace SchoolManagementSystemAPI.Services.General.GrpcController
{
    public class GradeSubjectGrpcController : GrpcGradeSubjectBase
    {
        private readonly IMapper _mapper;
        private readonly IClassSubjectService _service;

        public GradeSubjectGrpcController(IMapper mapper, IClassSubjectService service) 
        {
            _mapper = mapper;
            _service = service;
        }
        public override Task<GradeSubjectActionState> DeleteOneClassSubject(GetSingleGradeSubject request, ServerCallContext context)
        {
            GradeSubjectActionState resp = new();
            try
            {
                if (_service.UnEnrolSubjectTOClass(request.GradeNumber, request.SubjectTitle).GetAwaiter().GetResult())
                {
                    resp.Resp =true;
                    return Task.FromResult(resp);
                }
                throw new Exception("unable to Add subject");
            }
            catch (Exception ex) {
                resp.Resp = false;
                return Task.FromResult(resp);
            }
        }

        public override Task<ListGradeSubject> GetClassSubject(ByGradeNumber request, ServerCallContext context)
        {
            ListGradeSubject resp = new();
            var subjects = _service.GetClassSubjects(request.GradeNumber);
            foreach(var subject in subjects) { resp.GradeSubjects.Add(_mapper.Map<GradeSubjectDataGrpc>(subject)); }
            return Task.FromResult(resp);
        }

        public override Task<GradeSubjectDataGrpc> GetSingleClassSubject(GetSingleGradeSubject request, ServerCallContext context)
        {
            var subject = _service.GetSingleClassSubject(request.GradeNumber, request.SubjectTitle).GetAwaiter().GetResult();
            return Task.FromResult(_mapper.Map<GradeSubjectDataGrpc>(subject));

        }

        public override Task<GradeSubjectActionState> AddSujectToClass(GradeSubjectDataGrpc request, ServerCallContext context)
        {
            GradeSubjectActionState res = new();
            try
            {
                if (_service.EnrolSubjectTOClass(_mapper.Map<ClassSubjectDTO>(request)).GetAwaiter().GetResult())
                {
                    res.Resp = true;
                    return Task.FromResult(res);
                }
                throw new Exception("unable to Add subject");
            }
            catch
            {
                res.Resp = false;
                return Task.FromResult(res);
            }
        }
    }
}
