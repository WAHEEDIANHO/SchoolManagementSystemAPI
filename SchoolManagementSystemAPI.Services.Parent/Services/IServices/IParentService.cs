using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Parent.Services.IServices
{
    public interface IParentService
    {
        Task<IEnumerable<ParentDTO>> GetAllParent();
        Task<ParentDTO> GetParentById(string id);
        Task<bool> DeleteParentById(string id);
        //Task<bool> RegStudent(MsgRegParentDTO req);
    }
}
