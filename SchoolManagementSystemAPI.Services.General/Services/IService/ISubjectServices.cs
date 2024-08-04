using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface ISubjectServices
    {
        Task<IEnumerable<SubjectResponseDTO>> GetAllSubjects(Dictionary<string, string>? filter = null);
        Task<SubjectResponseDTO> GetSubjectByID(string id);
        Task<SubjectResponseDTO> DeleteSubjectByID(string id);
        Task<bool> CreateSubject(SubjectRequestDTO req);
    }
}
