using AutoMapper;
using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class NoteService: GeneralService<NoteDto, NoteResDto, Note>, INoteService
{
    private readonly IMapper _mapper;
    private readonly INoteRepository _repository;
    public NoteService(IMapper mapper, INoteRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<IEnumerable<NoteResDto>> GetStudentPersonalNotes(string studentId, string lessonId)
    {
        Dictionary<string, string> query = new()
        {
            {"StudentId", studentId},
            {"LessonId", lessonId}
        };
        return _mapper.Map<IEnumerable<NoteResDto>>(await _repository.GetAll(query));
    }
}