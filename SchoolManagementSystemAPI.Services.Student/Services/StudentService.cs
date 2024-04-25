using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Integrations.MessageBus;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Repositories;
using SchoolManagementSystemAPI.Services.Student.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IGrpcApplicationUserClientService _grpcApplication;
        private readonly string? hostname;
        private readonly string? userName;
        private readonly string? passWord;
        private readonly string? vHost;
        public StudentService(
            IStudentRepository repo, 
            IMapper mapper, 
            IUserService userService, 
            IConfiguration config,
            IGrpcApplicationUserClientService grpcApplication
            )
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService; 
            _config = config;
            _grpcApplication = grpcApplication;

            hostname = _config.GetValue<string>("RabbitmqConn:Host");
            userName = _config.GetValue<string>("RabbitmqConn:Username");
            passWord = _config.GetValue<string>("RabbitmqConn:Password");
            vHost = _config.GetValue<string>("RabbitmqConn:VirtualHost");
        }

        public async Task<bool> DeleteStudentById(string id)
        {
            try
            {
                var st = await _repo.GetById(id);
                if (st != null)
                {
                    _repo.Delete(st);
                    RMQMessageBus messenger = new RMQMessageBus(hostname, userName, passWord, vHost);
                    MsgRegStudentDTO msgDelStd = _mapper.Map<MsgRegStudentDTO>(st);
                    messenger.SendMessage(msgDelStd, _config.GetValue<string>("ExchnageAndQueueName:StudentDelQueue"), null);
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudent()
        {
            try
            {

                IEnumerable<StudentSchema> st = await _repo.GetAll();
                if (!st.IsNullOrEmpty())
                {
                    IEnumerable<UserResponseDTO> response = _grpcApplication.GetStudents(); //await _userService.getUserByRole();
                   var student = response.Join(st, x => x.Id.ToLower(), y => y.RegId.ToLower(), (x, y) => new StudentDTO(x, y));
                    return student;
                }
                else return Enumerable.Empty<StudentDTO>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<StudentDTO> GetByGrade(int GradeId)
        {
            try
            {
                IEnumerable<StudentSchema> st = _repo.GetByGrade(GradeId);
                if(!st.IsNullOrEmpty())
                {
                    IEnumerable<UserResponseDTO> students = _grpcApplication.GetStudents();
                    var gradeStudent = students.Join(st, x => x.Id.ToLower(), y => y.RegId.ToLower(), (x, y) => new StudentDTO(x, y));
                    return gradeStudent;
                }
                else return Enumerable.Empty<StudentDTO>();
            } 
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<StudentDTO> GetStudentById(string id)
        {
            try
            {
                StudentSchema st = await _repo.GetById(id);
                if (st != null)
                {
                    UserResponseDTO userDet = _grpcApplication.GetStudentById(id); //await _userService.getUserById(id.ToString());
                    StudentDTO res = new StudentDTO(userDet, st);
                    return res;
                }
                else return new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegStudent(MsgRegStudentDTO req)
        {
            try
            {
                StudentSchema st = _mapper.Map<StudentSchema>(req);
                await _repo.Add(st);
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
