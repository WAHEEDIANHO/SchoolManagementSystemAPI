using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Teacher.Services
{
    public class TeacherRegService
    {
        private readonly DbContextOptions _dbContextOptions;
        private IMapper _mapper;

        public TeacherRegService(DbContextOptions dbContextOptions, IMapper mapper)
        {
            _dbContextOptions = dbContextOptions;
            _mapper = mapper;
        }

        public async Task<bool> RegTeacher(MsgRegTeacherDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_dbContextOptions);
                //int dbCount = _repo.Set<StudentSchema>().Count() + 1;
                //req.AdmissionNo = string.Format("{0}{1:D4}{2:D3}", "SMS", DateTime.Now.ToString("yyyy"), dbCount);
                TeacherSchema st = _mapper.Map<TeacherSchema>(req);
                await _repo.AddAsync(st);
                await _repo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
