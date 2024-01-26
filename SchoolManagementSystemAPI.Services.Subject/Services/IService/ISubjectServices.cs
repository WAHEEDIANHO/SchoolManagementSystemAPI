using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
{
    public interface ISubjectServices
    {
        Task<IEnumerable<SubjectResponseDTO>> GetAllSubjects();
        Task<SubjectResponseDTO> GetSubjectByID(string id);
        Task<SubjectResponseDTO> DeleteSubjectByID(string id);
        Task<bool> CreateSubject(SubjectRequestDTO req);
    }
}
