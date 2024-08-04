using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Integrations.MessageBus;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;
using SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService;
using SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService.IGrpcService;

namespace SchoolManagementSystemAPI.Services.Teacher.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repo;
        private readonly IUserService _userService;
        private readonly IGrpcApplicationUserClientService _grpcApplicationUser;
        private readonly IConfiguration _config;
        private IMapper _mapper;
        private readonly string? hostname;
        private readonly string? userName;
        private readonly string? passWord;
        private readonly string? vHost;

        public TeacherService(
            ITeacherRepository repo, IMapper mapper, 
            IUserService userService, 
            IConfiguration config,
            IGrpcApplicationUserClientService grpcApplicationUser
            )
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _userService = userService;
            _grpcApplicationUser = grpcApplicationUser;
            
            hostname = _config.GetValue<string>("RabbitmqConn:Host");
            userName = _config.GetValue<string>("RabbitmqConn:Username");
            passWord = _config.GetValue<string>("RabbitmqConn:Password");
            vHost = _config.GetValue<string>("RabbitmqConn:VirtualHost");
        }

        public async Task<bool> DeleteTeacherById(string id)
        {
            try
            {
                var res = await _repo.GetTeacherById(id);
                if (res != null)
                {
                    _repo.Delete(res);
                    RMQMessageBus messenger = new RMQMessageBus(hostname, userName, passWord, vHost);
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
                    IEnumerable<UserResponseDTO> users = _grpcApplicationUser.GetTeachers();
                    return  users.Join(teacher, x => x.Id.ToLower(), y => y.RegId.ToLower(), (x, y) => new TeacherDTO(x, y));
                }else return Enumerable.Empty<TeacherDTO>();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TeacherDTO> GetTeacherById(string id)
        {
            try
            {
                TeacherSchema st = await _repo.GetTeacherById(id);
                if (st != null)
                {
                    UserResponseDTO user = _mapper.Map<UserResponseDTO>(_grpcApplicationUser.GetTeacherById(id)); ;
                    if (user == null) return new TeacherDTO(st);
                    return new TeacherDTO(user, st);
                }
                else return new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public Task<bool> RegTeacher(MsgRegTeacherDTO req)
        {
            throw new NotImplementedException();
        }
    }

}
