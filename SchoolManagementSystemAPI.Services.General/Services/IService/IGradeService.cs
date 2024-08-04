using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDTO>> GetAllClass(Dictionary<string, string>? filter = null);
        Task<GradeDTO> GetClassByKey(int id);
        Task<bool> DeleteClassbyId(int id);
        Task<bool> AddClass(GradeDTO stdClassDTO);
        Task<bool> UpdateClassTeacher(GradeDTO update);
    }
}
