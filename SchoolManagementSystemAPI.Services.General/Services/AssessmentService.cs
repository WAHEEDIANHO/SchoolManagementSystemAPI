using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class AssessmentService:GeneralService<Assessment, Assessment, Assessment>, IAssessmentService
{
    // private readonly IMapper _mapper;
    // private readonly IAssessmentRepository _repository;

    public AssessmentService(IMapper mapper, IAssessmentRepository repository): base(mapper, repository)
    {
        // _mapper = mapper;
        // _repository = repository;
    }
}