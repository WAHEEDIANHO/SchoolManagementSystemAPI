using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDTO>> getAllClass();
        Task<GradeDTO> getClassByKey(int id);
        Task<bool> deleteClassbyId(int id);
        Task<bool> AddClass(GradeDTO stdClassDTO);
        Task<bool> updateClassTeacher(GradeDTO update);
    }
}
