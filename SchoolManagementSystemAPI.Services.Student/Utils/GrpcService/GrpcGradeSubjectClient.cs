using AutoMapper;
using Grpc.Net.Client;
using SchoolManagementSystemAPI.Services.Student.Model.Dto;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService
{
    public class GrpcGradeSubjectClient : IGrpcGradeSubjectClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public GrpcGradeSubjectClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClassSubjectDTO>> GetClassSubject(int GradeNumber)
        {
            var channel = GrpcChannel.ForAddress(_config["GeneralSessionGrpc"]);
            var client = new GrpcGradeSubject.GrpcGradeSubjectClient(channel); //new GrpcApplicationUser.GrpcApplicationUserClient(channel);

            try
            {
                ByGradeNumber req = new();
                req.GradeNumber = GradeNumber;
                var resp = await client.GetClassSubjectAsync(req);
                return _mapper.Map<IEnumerable<ClassSubjectDTO>>(resp.GradeSubjects);
            }catch (Exception ex)
            {
                Console.WriteLine($"--------> Errror {ex.ToString()}");
                return Enumerable.Empty<ClassSubjectDTO>();
            }
        }
    }
}
