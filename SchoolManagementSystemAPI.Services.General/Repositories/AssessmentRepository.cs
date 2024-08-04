using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class AssessmentRepository: GenericRepository<Assessment, AppDbContext>, IAssessmentRepository
{
    // private readonly AppDbContext _context;
    public AssessmentRepository(AppDbContext context): base(context){}
}