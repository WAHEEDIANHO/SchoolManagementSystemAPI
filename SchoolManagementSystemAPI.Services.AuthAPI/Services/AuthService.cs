using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolManagementSystem.Integrations.MessageBus;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Services
{
    public class AuthService<T> : IAuthService<T> where T : RegisterRequestDTO
    {
        public readonly IAuthRepository _authRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        //private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly string? hostname;
        private readonly string? userName;
        private readonly string? passWord;
        private readonly string? vHost;

        public AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator, IConfiguration config, IMapper mapper)
        {
            _authRepository = authRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
           // _logger = logger;
            _config = config;
            _mapper = mapper;

            hostname = _config.GetValue<string>("RabbitmqConn:Host");
            userName = _config.GetValue<string>("RabbitmqConn:Username");
            passWord = _config.GetValue<string>("RabbitmqConn:Password");
            vHost = _config.GetValue<string>("RabbitmqConn:VirtualHost");
        }
       

        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            try
            {
                var user = await _authRepository.GetUserByUsername(requestDTO.username);
                bool isValid = await _authRepository.CheckPassword(user, requestDTO.password);

                if (user == null || !isValid)
                {       
                    return new LoginResponseDTO() { id = "", token = "" };
                }

                var roles = await _authRepository.GetRoles(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);
                
                return new LoginResponseDTO() { id = user.Id, token = token, };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Register(T registerRequestDTO)
        {
            
            try
            {
                bool isRegister = await _authRepository.RegisterUser((RegisterRequestDTO) registerRequestDTO);
                bool isRoleAssign = await _authRepository.AssignRole((RegisterRequestDTO) registerRequestDTO);



                if(isRegister && isRoleAssign != null) {
                    ApplicationUser user;
                    RMQMessageBus message;
                    switch (registerRequestDTO.Role)
                    {
                        case "STUDENT":
                            StudentRegisterDTO student = (StudentRegisterDTO)(RegisterRequestDTO) registerRequestDTO;
                            //_logger.LogInformation("Brodcasting ur message STUDENT");

                            user = await _authRepository.GetUserByUsername(registerRequestDTO.Email);
                            MsgRegStudentDTO msgStudentReg = new()
                            {
                                AdmissionNo = "",
                                ClassId = student.ClassId,
                                SessionId = student.SessionId,
                                RegId = user.Id
                            };
                            Console.WriteLine($"--------> Hostname {hostname}");
                            message = new RMQMessageBus(hostname, userName, passWord, vHost);
                            message.SendMessage(msgStudentReg, _config.GetValue<string>("ExchnageAndQueueName:StudentRegQueue"), null);
                           
                            // student creation goes here
                            break;
                      
                        case "TEACHER":
                            TeacherRegisterDTO teacher = (TeacherRegisterDTO)(RegisterRequestDTO)registerRequestDTO;
                            //Console.WriteLine("Brodcasting ur message TEACHER");

                            user = await _authRepository.GetUserByUsername(registerRequestDTO.Email);
                            MsgRegTeacherDTO msgRegTeacher = new()
                            {
                                AppointmentDate = teacher.AppointmentDate,
                                CourseOfStudy = teacher.CourseOfStudy,
                                Grade = teacher.Grade,
                                LevelOfStudy = teacher.LevelOfStudy,
                                RegId = user.Id,
                            };
                            //message = new RMQMessageBus("rabbitmq-sms-srv", "guest", "guest", "/");
                            message = new RMQMessageBus(hostname, userName, passWord, vHost);
                            Console.WriteLine("------> Conn set successfully about to send the meassage");
                            message.SendMessage(msgRegTeacher, _config.GetValue<string>("ExchnageAndQueueName:TeacherRegQueue"), null);
                            //teacers creation goes here
                            break;
                        case "PARENT":
                            ParentRegistrationDTO parent = (ParentRegistrationDTO) (RegisterRequestDTO) registerRequestDTO;
                            //Console.WriteLine("Brodcasting ur message PARENT");

                            user = await _authRepository.GetUserByUsername(registerRequestDTO.Email);
                            MsgRegParentDTO msgParentReg = new()
                            {
                                RegId = user.Id,
                                Occupation = parent.Occupation
                            };
                            message = new RMQMessageBus();
                            message.SendMessage(msgParentReg, _config.GetValue<string>("ExchnageAndQueueName:ParentRegQueue"), null);
                            //teacers creation goes here
                            break;
                        default:
                            Console.WriteLine("No action");
                            break;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                if (ex is BadHttpRequestException && ex.Message.IndexOf("already taken") >= 0) throw;
                try
                {
                    ApplicationUser user = await _authRepository.GetUserByUsername(registerRequestDTO.Email);
                    _authRepository.Remove(user);
                } catch (Exception e) { }

               throw;
            }
        }

        public string CreateRole(string role)
        {
            try
            {
                return _authRepository.CreateRole(role);
            }catch(Exception ex) {
                throw;
            }
        }
    
        public async Task<UserResponseDTO> GetUser(string id)
        {
            try
            {
                var user = await _authRepository.GetUserById(id);
                return _mapper.Map<UserResponseDTO>(user);  
            }catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<UserResponseDTO> GetUsersByRole(string role)
        {
            try
            {
                if (string.IsNullOrEmpty(role)) return Enumerable.Empty<UserResponseDTO>();
                var res =  _authRepository.GetUsersByRole(role);
                return _mapper.Map<IEnumerable<UserResponseDTO>>(res);
            }catch(Exception ex) { throw; };
        }

        public IEnumerable<UserResponseDTO> GetUsersByGender(string gender)
        {
            try
            {
                if (string.IsNullOrEmpty(gender)) return Enumerable.Empty<UserResponseDTO>();
                var res = _authRepository.GetUsersByRole(gender);
                return _mapper.Map<IEnumerable<UserResponseDTO>>(res);
            }
            catch (Exception ex) { throw; };
        }

        public IEnumerable<UserResponseDTO> GetAllUsers()
        {
            try
            {
                var res = _authRepository.GetAllUsers();
                return _mapper.Map<IEnumerable<UserResponseDTO>>(res);
            }
            catch (Exception ex) { throw; };

        }

        public async Task<bool> DelUser(String  id)
        {
            try
            {
                ApplicationUser user = await _authRepository.GetUserById(id);
                _authRepository.Remove(user);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
