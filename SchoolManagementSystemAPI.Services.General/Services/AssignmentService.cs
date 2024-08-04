using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class AssignmentService: GeneralService<Assignment, Assignment, Assignment>, IAssignmentService
{
    // private readonly IMapper _mapper;
    // private readonly IAssignmentRepository _repository;

    public AssignmentService(IMapper mapper, IAssignmentRepository repository): base(mapper, repository)
    {
        // _mapper = mapper;
        // _repository = repository;
    }
}