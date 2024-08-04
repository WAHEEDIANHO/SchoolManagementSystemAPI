using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class NoteRepository: GenericRepository<Note, AppDbContext>, INoteRepository
{
    public NoteRepository(AppDbContext context): base(context) {}
}