using SchoolManagementSystemAPI.Services.Student.Model.Dto;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService
{
    public interface IGrpcGradeSubjectClient
    {
       Task<IEnumerable<ClassSubjectDTO>> GetClassSubject(int GradeNumber);
    }
}
