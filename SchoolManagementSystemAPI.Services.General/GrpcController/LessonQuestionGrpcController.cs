using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using Waheedianho.Protobuf.Types;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class LessonQuestionGrpcController: GrpcLessonQuestion.GrpcLessonQuestionBase
{
    private readonly IMapper _mapper;
    private readonly ILessonQuestionService _service;

    public LessonQuestionGrpcController(IMapper mapper, ILessonQuestionService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public override Task<LessonQuestionGrpcList> GetAllLessonQuestion(Query request, ServerCallContext context)
    {
        LessonQuestionGrpcList resp = new();
        var lessonQuestions = _service.GetAll().GetAwaiter().GetResult();
        foreach (var lessonQuestion in lessonQuestions)
            resp.LessonQuestionGrpc.Add(_mapper.Map<LessonQuestionGrpc>(lessonQuestion));
        return Task.FromResult(resp);
    }

    public override Task<Empty> CreateLessonQuestion(LessonQuestionDtoGrpc request, ServerCallContext context)
    {
        var lessonQuestion = _mapper.Map<LessonQuestion>(request);
        _service.Add(lessonQuestion).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }

    public override Task<LessonQuestionGrpc> GetLessonQuestionById(LessonQuestionId request, ServerCallContext context)
    {
        var lessonQuestion = _service.GetById(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(_mapper.Map<LessonQuestionGrpc>(lessonQuestion));
    }

    public override Task<Empty> DeleteLessonQuestion(LessonQuestionId request, ServerCallContext context)
    {
        _service.Delete(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
}