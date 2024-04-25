using AutoMapper;
using Grpc.Net.Client;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Parent.Protos
{
    public class GrpcApplicationUserClientService : IGrpcApplicationUserClientService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public GrpcApplicationUserClientService(IConfiguration config, IMapper mapper) 
        {
            _config = config;
            _mapper=mapper;
        }
        public IEnumerable<UserResponseDTO> GetAllParents()
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var client = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserRole role = new UserRole { Role = "PARENT"};
                var resp = client.GetUsersByRole(role);
                return _mapper.Map<IEnumerable<UserResponseDTO>>(resp);
            }catch (Exception ex) {
                Console.WriteLine($"------> Error {ex.ToString()}");
                return Enumerable.Empty<UserResponseDTO>();
            }
        }

        public UserResponseDTO GetUserById(string id)
        {
            var channel = GrpcChannel.ForAddress(_config["AppSUerGrpc"]);
            var client = new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                UserId userId = new UserId { Id = id };
                var resp = client.GetUserById(userId);
                return _mapper.Map<UserResponseDTO>(resp);
            }catch(Exception ex)
            {
                Console.WriteLine($"-----> error occured ${ex.ToString()}");
                return null;
            }
        }
    }
}
