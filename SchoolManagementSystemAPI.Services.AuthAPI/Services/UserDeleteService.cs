using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Services
{
    public class UserDeleteService
    {
        private readonly DbContextOptions _dbContextOption;
        private readonly IMapper _mapper;

        public UserDeleteService(DbContextOptions dbContextOption, IMapper mapper)
        {
            _dbContextOption = dbContextOption;
            _mapper = mapper;
        }

        public async Task<bool> DelUser(MsgRegTeacherDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_dbContextOption);
                ApplicationUser user = await _repo.Set<ApplicationUser>().FirstAsync(x => x.Id.ToLower() == req.RegId.ToLower());
                _repo.Remove(user);
                await _repo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DelUser(MsgRegStudentDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_dbContextOption);
                ApplicationUser user = await _repo.Set<ApplicationUser>().FirstAsync(x => x.Id.ToLower() == req.RegId.ToLower());
                _repo.Remove(user);
                await _repo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DelUser(MsgRegParentDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_dbContextOption);
                ApplicationUser user = await _repo.Set<ApplicationUser>().FirstAsync(x => x.Id.ToLower() == req.RegId.ToLower());
                _repo.Remove(user);
                await _repo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


//int dbCount = _repo.Set<StudentSchema>().Count() + 1;
//req.AdmissionNo = string.Format("{0}{1:D4}{2:D3}", "SMS", DateTime.Now.ToString("yyyy"), dbCount);
