using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Parent.Repositories.Data;
using SchoolManagementSystemAPI.Services.Parent.Repositories.IRepositories;

namespace SchoolManagementSystemAPI.Services.Parent.Repositories
{
    public class ParentRepository: GenericRepository<ParentSchema, AppDbContext>, IParentRepository
    {
        private readonly AppDbContext _context;

        public ParentRepository(AppDbContext context) : base(context) { _context = context; }

        public async Task<ParentSchema> GetParentById(string id)
        {
            return await _context.Parent.FirstAsync(x => x.RegId == id);
        }
    }
}
