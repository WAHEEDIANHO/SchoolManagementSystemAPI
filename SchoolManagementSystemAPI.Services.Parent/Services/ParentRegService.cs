using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Repositories;
using SchoolManagementSystemAPI.Services.Parent.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Parent.Services
{
    public class ParentRegService
    {
        private readonly DbContextOptions _dbContextOptions;
        private readonly IMapper _mapper;


        public ParentRegService(DbContextOptions context, IMapper mapper)
        {
           _dbContextOptions = context;
            _mapper = mapper;
        }
        public async Task<bool> RegParent(MsgRegParentDTO req)
        {
            try
            {
                await using var _repo = new AppDbContext(_dbContextOptions);
                //int dbCount = _repo.Set<StudentSchema>().Count() + 1;
                //req.AdmissionNo = string.Format("{0}{1:D4}{2:D3}", "SMS", DateTime.Now.ToString("yyyy"), dbCount);
                ParentSchema st = _mapper.Map<ParentSchema>(req);
                await _repo.AddAsync(st);
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
