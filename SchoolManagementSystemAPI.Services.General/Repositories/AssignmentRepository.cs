using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class AssignmentRepository: GenericRepository<Assignment, AppDbContext>, IAssignmentRepository
{
    public AssignmentRepository(AppDbContext context) : base(context)
    {
    }
}