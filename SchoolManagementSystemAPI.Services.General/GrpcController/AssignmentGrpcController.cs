using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using Waheedianho.Protobuf.Types;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class AssignmentGrpcController: GrpcAssignment.GrpcAssignmentBase
{
    private readonly Mapper _mapper;
    private readonly IAssignmentService _service;

    public AssignmentGrpcController(Mapper mapper, IAssignmentService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public override Task<AssignmentGrpcList> GetAllAssignment(Query request, ServerCallContext context)
    {
        AssignmentGrpcList resp = new();
        var assignments = _service.GetAll().GetAwaiter().GetResult();
        foreach (var assignment in assignments)
            resp.AssignmentGrpc.Add(_mapper.Map<AssignmentGrpc>(assignment));
        return Task.FromResult(resp);
    }

    public override Task<Empty> CreateAssignment(AssignmentDtoGrpc request, ServerCallContext context)
    {
        Assignment assignment = _mapper.Map<Assignment>(request);
        _service.Add(assignment).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }

    public override Task<AssignmentGrpc> GetAssignmentById(AssignmentId request, ServerCallContext context)
    {
        var assignment = _service.GetById(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(_mapper.Map<AssignmentGrpc>(assignment));
    }

    public override Task<Empty> DeleteAssignment(AssignmentId request, ServerCallContext context)
    {
        _service.Delete(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
}