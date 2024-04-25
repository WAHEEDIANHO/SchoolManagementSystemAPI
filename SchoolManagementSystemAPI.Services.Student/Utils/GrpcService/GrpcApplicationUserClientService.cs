using AutoMapper;
using Grpc.Net.Client;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService
{
    public class GrpcApplicationUserClientService : IGrpcApplicationUserClientService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public GrpcApplicationUserClientService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
        
        public UserResponseDTO GetStudentById(string id)
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var client = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserId userId = new UserId { Id = id };
                var response = client.GetUserById(userId);
                return _mapper.Map<UserResponseDTO>(response);
            }catch (Exception ex)
            {
                Console.WriteLine($"--------> Errror {ex.ToString()}");
                return null;
            }
        }

        public IEnumerable<UserResponseDTO> GetStudents()
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var client = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserRole role = new UserRole() { Role = "STUDENT"};
                var response = client.GetUsersByRole(role);
                return _mapper.Map<IEnumerable<UserResponseDTO>>(response.Users);

            }catch(Exception ex) {
                Console.WriteLine($"--------> Errror {ex.ToString()}");
                return Enumerable.Empty<UserResponseDTO>();
            }
        }
    }
}
