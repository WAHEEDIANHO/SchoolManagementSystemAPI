using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface ITermService: IGeneralService<TermDto, TermDto, Term>
{
    public Task<bool> Delete(string sessionName, int termNumber);
    public Task<TermDto> GetCurrentTerm();
}