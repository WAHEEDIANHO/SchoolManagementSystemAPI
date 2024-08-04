using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class TermRepository: GenericRepository<Term, AppDbContext>, ITermRepository
{
    private readonly AppDbContext _context;
    public TermRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Term> GetByKey(string sessionName, int termNumber)
    {
        return  await _context.Set<Term>()
            .FirstAsync(u => u.SessionName == sessionName && u.TermNumber == termNumber );
    }

    public async Task<Term> GetCurrentTerm()
    {
        return await _context.Set<Term>().FirstAsync(x => x.TermStatus == TermStatus.Ongoing);
    }
}