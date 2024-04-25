using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class SubjectRepository : GenericRepository<Subject, AppDbContext>, ISubjectRepository
    {       
        public SubjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
