using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using Waheedianho.Protobuf.Types;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class AssessmentGrpcController: GrpcAssessment.GrpcAssessmentBase
{
    private readonly IMapper _mapper;
    private readonly IAssessmentService _service;

    public AssessmentGrpcController(IMapper mapper, IAssessmentService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public override Task<AssessmentGrpcList> GetAllAssessment(Query request, ServerCallContext context)
    {
        AssessmentGrpcList resp = new();
        var assessments = _service.GetAll().GetAwaiter().GetResult();
        foreach (var assessment in assessments)
            resp.AssessmentGrpc.Add(_mapper.Map<AssessmentGrpc>(assessment));
        return Task.FromResult(resp);
    }

    public override Task<Empty> CreateAssessment(AssessmentDtoGrpc request, ServerCallContext context)
    {
        Assessment assessment = _mapper.Map<Assessment>(request);
        _service.Add(assessment).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }

    public override Task<AssessmentGrpc> GetAssessmentById(AssessmentId request, ServerCallContext context)
    {
        AssessmentGrpc resp = new();
        var assessment = _service.GetById(request.Id).GetAwaiter().GetResult();
        resp = _mapper.Map<AssessmentGrpc>(assessment);
        return Task.FromResult(resp);
    }

    public override Task<Empty> DeleteAssessment(AssessmentId request, ServerCallContext context)
    {
        _service.Delete(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
}