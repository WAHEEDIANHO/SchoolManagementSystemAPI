using AutoMapper;
using Grpc.Net.Client;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.TeacherAPI;

namespace SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService
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

        public IEnumerable<UserResponseDTO> GetTeachers()
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var clinet = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserRole req = new UserRole { Role = "TEACHER" };
                var resp = clinet.GetUsersByRole(req);
                return _mapper.Map<IEnumerable<UserResponseDTO>>(resp.Users);
            }catch (Exception e)
            {
                Console.WriteLine($"----> There is an Error {e.ToString()}");
                return Enumerable.Empty<UserResponseDTO>();
            }
        }
   
        public UserResponseDTO GetTeacherById(string id)
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var client = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserId userId = new UserId { Id = id };
                var response = client.GetUserById(userId);
                return _mapper.Map<UserResponseDTO>(response);
            }catch (Exception e)
            {
                Console.WriteLine($"----> There is an Error {e.ToString()}");
                return null;
            }
        }
    }
}
