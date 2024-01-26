using GenericRepository;
using SchoolManagementSystemAPI.Services.Parent.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Parent.Repositories.IRepositories
{
    public interface IParentRepository: IGenericRepository<ParentSchema, AppDbContext>
    {
        Task<ParentSchema> GetParentById(string id);
    }
}
