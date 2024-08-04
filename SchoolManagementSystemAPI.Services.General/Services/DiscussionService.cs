using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class DiscussionService: GeneralService<DiscussionReqDto, Discussion, Discussion>, IDiscussionService
{
    public DiscussionService(IMapper mapper, IDiscussionRepository repository) : base(mapper, repository)
    {
        
    }
    
}