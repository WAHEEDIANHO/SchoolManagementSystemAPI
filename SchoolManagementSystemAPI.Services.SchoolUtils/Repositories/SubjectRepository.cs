using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories
{
    public class SubjectRepository : GenericRepository<Subject, AppDbContext>, ISubjectRepository
    {       
        public SubjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
