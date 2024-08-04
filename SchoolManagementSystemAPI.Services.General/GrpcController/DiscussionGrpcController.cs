using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using Waheedianho.Protobuf.Types;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class DiscussionGrpcController: GrpcDiscussion.GrpcDiscussionBase
{
    private readonly IMapper _mapper;
    private readonly IDiscussionService _service;

    public DiscussionGrpcController(IMapper mapper, IDiscussionService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public override Task<DiscussionGrpcList> GetAllDiscussion(Query request, ServerCallContext context)
    {
        DiscussionGrpcList resp = new();
        var discussions = _service.GetAll().GetAwaiter().GetResult();
        foreach (var discussion in discussions)
            resp.DiscussionGrpc.Add(_mapper.Map<DiscussionGrpc>(discussion));
        return Task.FromResult(resp);
    }

    public override Task<Empty> CreateDiscussion(DiscussionDtoGrpc request, ServerCallContext context)
    {
        var discussion = _mapper.Map<DiscussionReqDto>(request);
        _service.Add(discussion).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }

    public override Task<DiscussionGrpc> GetDiscussionById(DiscussionId request, ServerCallContext context)
    {
        var discussion = _service.GetById(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(_mapper.Map<DiscussionGrpc>(discussion));
    }

    public override Task<Empty> DeleteDiscussion(DiscussionId request, ServerCallContext context)
    {
        _service.Delete(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
}