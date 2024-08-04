using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class TermService: GeneralService<TermDto, TermDto, Term>, ITermService
{
    private readonly IMapper _mapper;
    private readonly ITermRepository _repository;
    public TermService(IMapper mapper, ITermRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<bool> Delete(string sessionName, int termNumber)
    {
        var term = await _repository.GetByKey(sessionName, termNumber);
         _repository.Delete(term);
         return true;
    }

    public async Task<TermDto> GetCurrentTerm()
    {
        return _mapper.Map<TermDto>(await _repository.GetCurrentTerm());
    }
    
}