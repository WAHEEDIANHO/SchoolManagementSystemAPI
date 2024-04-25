using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using SchoolManagementSystemAPI.Services.Student.Model.Dto;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService;

public class GrpcWebinarClientService: IGrpcWebinarClientService
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public GrpcWebinarClientService(IConfiguration config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber)
    {
        var channel = GrpcChannel.ForAddress(_config["GeneralSessionGrpc"]);
        var client = new GrpcWebinar.GrpcWebinarClient(channel);

        try
        {
            GradeNumber req = new(){ Num = GradeNumber};
            var resp = await client.GetUpcomingWebinarsAsync(req);
            return _mapper.Map<IEnumerable<Webinar>>(resp.Webinars);
        }catch (Exception ex)
        {
            Console.WriteLine($"--------> Errror {ex.ToString()}");
            return Enumerable.Empty<Webinar>();
        }
    }

    public async Task<Webinar> GetCurrentWebinar(int GradeNumber)
    {
        var channel = GrpcChannel.ForAddress(_config["GeneralSessionGrpc"]);
        var client = new GrpcWebinar.GrpcWebinarClient(channel);

        try
        {
            GradeNumber req = new(){ Num = GradeNumber};
            var resp = await client.GetCurrentWebinarAsync(req);
            return _mapper.Map<Webinar>(resp);
        }
        catch (RpcException ex) when(ex.StatusCode == StatusCode.NotFound)
        {
            Console.WriteLine($"--------> Errror {ex.ToString()}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}