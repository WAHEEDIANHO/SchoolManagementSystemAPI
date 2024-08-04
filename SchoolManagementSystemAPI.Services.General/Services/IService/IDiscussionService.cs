

using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface IDiscussionService: IGeneralService<DiscussionReqDto, Discussion, Discussion>
{
    
}