using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Repositories;
using SchoolManagementSystemAPI.Services.Student.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Student.Services
{
    public class StuRegService
    {
        private readonly DbContextOptions<AppDbContext> _context;
        private IMapper _mapper;
        public StuRegService(DbContextOptions<AppDbContext> context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<bool> RegStudent(MsgRegStudentDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_context);
                int dbCount = _repo.Set<StudentSchema>().Count() + 1;
                req.AdmissionNo = string.Format("{0}{1:D4}{2:D3}", "SMS", DateTime.Now.ToString("yyyy"), dbCount);
                StudentSchema st = _mapper.Map<StudentSchema>(req);
                _repo.Add(st);
                await _repo.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
