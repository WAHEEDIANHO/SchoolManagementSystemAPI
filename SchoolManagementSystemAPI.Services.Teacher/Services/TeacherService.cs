using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Integrations.MessageBus;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Teacher.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repo;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private IMapper _mapper;

        public TeacherService(ITeacherRepository repo, IMapper mapper, IUserService userService, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _userService = userService;
        }

        public async Task<bool> DeleteStudentById(string id)
        {
            try
            {
                var res = await _repo.GetByKey(id);
                if (res != null)
                {
                    _repo.Delete(res);
                    RMQMessageBus messenger = new();
                    MsgRegTeacherDTO msg = _mapper.Map<MsgRegTeacherDTO>(res);
                    messenger.SendMessage(msg, _config.GetValue<string>("ExchnageAndQueueName:TeacherDelQueue"), null);
                    return true;
                }else return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TeacherDTO>> GetAllTeacher()
        {
            try
            {
                IEnumerable<TeacherSchema> teacher = await _repo.GetAll();
                if (!teacher.IsNullOrEmpty())
                {
                    IEnumerable<UserResponseDTO> users = await _userService.getUserByRole();
                    return  users.Join(teacher, x => x.Id.ToLower(), y => y.RegId.ToLower(), (x, y) => new TeacherDTO(x, y));
                }else return Enumerable.Empty<TeacherDTO>();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TeacherDTO> GetStudentById(string id)
        {
            try
            {
                TeacherSchema st = await _repo.GetTeacherById(id);
                if (st != null)
                {
                    UserResponseDTO user = await _userService.getUserById(id);
                    return new TeacherDTO(user, st);
                }
                else return new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> RegStudent(MsgRegTeacherDTO req)
        {
            throw new NotImplementedException();
        }
    }

}
