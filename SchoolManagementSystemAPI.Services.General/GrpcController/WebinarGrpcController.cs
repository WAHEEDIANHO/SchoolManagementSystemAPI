using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class WebinarGrpcController: GrpcWebinar.GrpcWebinarBase
{
    private readonly IMapper _mapper;
    private readonly IWebinarService _service;

    public WebinarGrpcController(IMapper mapper, IWebinarService service)
    {
        _mapper = mapper;
        _service = service;
    }
    public override Task<EmptyGrpc> AddWebinar(WebinarReqGrpcDto request, ServerCallContext context)
    {
        EmptyGrpc resp = new();
        WebinarReqDto newWebinar = _mapper.Map<WebinarReqDto>(request);
        _service.AddWebinar(newWebinar).GetAwaiter().GetResult();
        return Task.FromResult(resp);
    }

    public override Task<EmptyGrpc> RemoveWebinar(WebinarId request, ServerCallContext context)
    {
        EmptyGrpc resp = new();
        _service.RemoveWebinar(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(resp);
    }

    public override Task<WebinarListGrpc> GetUpcomingWebinars(GradeNumber request, ServerCallContext context)
    {
        WebinarListGrpc resp = new();
        var webinars = _service.GetUpcomingWebinars(request.Num).GetAwaiter().GetResult();
        foreach (var webinar in webinars) { resp.Webinars.Add(_mapper.Map<WebinargRPC>(webinar)); }
        return Task.FromResult(resp);
    }

    public override Task<WebinargRPC> GetCurrentWebinar(GradeNumber request, ServerCallContext context)
    {
        WebinargRPC resp = new();
        var webinar = _mapper.Map<WebinargRPC>(_service.GetCurrentWebinar(request.Num).GetAwaiter().GetResult());
        if (webinar == null) { webinar = new WebinargRPC(); }   
        return Task.FromResult(webinar);
    }
}