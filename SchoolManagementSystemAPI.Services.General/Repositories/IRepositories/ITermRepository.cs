using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;

public interface ITermRepository: IGenericRepository<Term, AppDbContext>
{
    public Task<Term> GetByKey(string sessionName, int termNumber);
    public Task<Term> GetCurrentTerm();

}