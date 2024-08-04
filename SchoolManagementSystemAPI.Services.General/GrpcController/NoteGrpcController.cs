using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;
using Waheedianho.Protobuf.Types;

namespace SchoolManagementSystemAPI.Services.General.GrpcController;

public class NoteGrpcController: GrpcNote.GrpcNoteBase
{
    private readonly IMapper _mapper;
    private readonly INoteService _service;

    public NoteGrpcController(IMapper mapper, INoteService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    public override Task<NoteGrpcList> GetAllNote(Query request, ServerCallContext context)
    {
        NoteGrpcList resp = new();
        var notes = _service.GetAll().GetAwaiter().GetResult();
        foreach (var note in notes)
            resp.NoteGrpc.Add(_mapper.Map<NoteGrpc>(note));
        return Task.FromResult(resp);
    }
    
    public override Task<Empty> CreateNote(NoteDtoGrpc request, ServerCallContext context)
    {
        var note = _mapper.Map<NoteDto>(request);
        _service.Add(note).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
    
    public override Task<NoteGrpc> GetNoteById(NoteId request, ServerCallContext context)
    {
        var note = _service.GetById(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(_mapper.Map<NoteGrpc>(note));
    }
    
    public override Task<Empty> DeleteNote(NoteId request, ServerCallContext context)
    {
        _service.Delete(request.Id).GetAwaiter().GetResult();
        return Task.FromResult(new Empty());
    }
}