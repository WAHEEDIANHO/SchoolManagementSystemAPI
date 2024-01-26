using AutoMapper;
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

        public AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator, IConfiguration config, IMapper mapper)
        {
            _authRepository = authRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
           // _logger = logger;
            _config = config;
            _mapper = mapper;
        }
       

        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            try
            {
                var user = await _authRepository.GetUserById(requestDTO.username);
                bool isValid = await _authRepository.CheckPassword(user, requestDTO.password);

                if (user == null || !isValid)
                {
                    return new LoginResponseDTO() { user = new(), token = "" };
                }

                var roles = await _authRepository.GetRoles(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                UserDTO userDTO = new()
                {
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    id = user.Id,
                };

                return new LoginResponseDTO() { user = userDTO, token = token, };
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

                            user = await _authRepository.GetUserById(registerRequestDTO.Email);
                            MsgRegStudentDTO msgStudentReg = new()
                            {
                                AdmissionNo = "",
                                ClassId = student.ClassId,
                                SessionId = student.SessionId,
                                RegId = user.Id
                            };
                            message = new RMQMessageBus();
                            message.SendMessage(msgStudentReg, _config.GetValue<string>("ExchnageAndQueueName:StudentRegQueue"), null);
                           
                            // student creation goes here
                            break;
                      
                        case "TEACHER":
                            TeacherRegisterDTO teacher = (TeacherRegisterDTO)(RegisterRequestDTO)registerRequestDTO;
                            //Console.WriteLine("Brodcasting ur message TEACHER");

                            user = await _authRepository.GetUserById(registerRequestDTO.Email);
                            MsgRegTeacherDTO msgRegTeacher = new()
                            {
                                AppointmentDate = teacher.AppointmentDate,
                                CourseOfStudy = teacher.CourseOfStudy,
                                Grade = teacher.Grade,
                                LevelOfStudy = teacher.LevelOfStudy,
                                RegId = user.Id,
                            };
                            message = new RMQMessageBus();
                            message.SendMessage(msgRegTeacher, _config.GetValue<string>("ExchnageAndQueueName:TeacherRegQueue"), null);
                            //teacers creation goes here
                            break;
                        case "PARENT":
                            ParentRegistrationDTO parent = (ParentRegistrationDTO) (RegisterRequestDTO) registerRequestDTO;
                            //Console.WriteLine("Brodcasting ur message PARENT");

                            user = await _authRepository.GetUserById(registerRequestDTO.Email);
                            MsgRegParentDTO msgParentReg = new()
                            {
                                RegId = user.Id,
                                Occupation = parent.Occupation
                            };
                            message = new RMQMessageBus();
                            message.SendMessage(msgParentReg, _config.GetValue<string>("ExchnageAndQueueName:ParentRegQueue"), null);
                            //teacers creation goes here
                            break;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
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

        public IEnumerable<UserResponseDTO> GetAllUsers()
        {
            try
            {
                var res = _authRepository.GetAllUsers();
                return _mapper.Map<IEnumerable<UserResponseDTO>>(res);
            }
            catch (Exception ex) { throw; };

        }
    }
}
