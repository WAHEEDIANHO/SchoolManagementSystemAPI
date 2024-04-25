using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Integrations.MessageBus;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Repositories;
using SchoolManagementSystemAPI.Services.Parent.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Parent.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Parent.Services
{
    public class ParentService: IParentService
    {
        private readonly IParentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly string? hostname;
        private readonly string? userName;
        private readonly string? passWord;
        private readonly string? vHost;

        public ParentService(IParentRepository repo, IMapper mapper, IUserService userService, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
            _config = config;

            hostname = _config.GetValue<string>("RabbitmqConn:Host");
            userName = _config.GetValue<string>("RabbitmqConn:Username");
            passWord = _config.GetValue<string>("RabbitmqConn:Password");
            vHost = _config.GetValue<string>("RabbitmqConn:VirtualHost");
        }

       

        public async Task<bool> DeleteParentById(string id)
        {
            try
            {
                var res = await _repo.GetParentById(id);
                if (res != null)
                {
                    _repo.Delete(res);
                    RMQMessageBus messenger = new RMQMessageBus(hostname, userName, passWord, vHost);
                    MsgRegParentDTO msg = _mapper.Map<MsgRegParentDTO>(res);
                    messenger.SendMessage(msg, _config.GetValue<string>("ExchnageAndQueueName:ParentDelQueue"), null);
                    return true;
                }return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ParentDTO>> GetAllParent()
        {
            try
            {
                IEnumerable<ParentSchema> st = await _repo.GetAll();
                if(!st.IsNullOrEmpty())
                {
                    IEnumerable<UserResponseDTO> users = await _userService.GetParent();
                    return users.Join(st, x => x.Id.ToLower(), y => y.RegId.ToLower(), (x, y) => new ParentDTO(x, y));
                }else return Enumerable.Empty<ParentDTO>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

        public async Task<ParentDTO> GetParentById(string id)
        {
            try
            {
                ParentSchema st = await _repo.GetParentById(id);
                if (st != null)
                {
                    UserResponseDTO parent = await _userService.GetParentById(id);
                    return new ParentDTO(parent, st);
                }
                else return new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
