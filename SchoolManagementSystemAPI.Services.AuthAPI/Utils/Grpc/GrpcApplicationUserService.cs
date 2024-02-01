using AutoMapper;
using Grpc.Core;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.IRepositories;
using static SchoolManagementSystemAPI.Services.AuthAPI.GrpcApplicationUser;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Utils.Grpc
{
    public class GrpcApplicationUserService: GrpcApplicationUserBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;

        public GrpcApplicationUserService(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<AppliactionUserResponse> GetUsersByRole(UserRole request, ServerCallContext context)
        {
            var response = new AppliactionUserResponse();
            var users = _repository.GetUsersByRole(request.Role); //instance of ApplicatioUser
            foreach (var user in users) response.Users.Add(_mapper.Map<GrpcApplicationUserModel>(user));
            return Task.FromResult(response);
        }

        public override Task<GrpcApplicationUserModel> GetUserById(UserId request, ServerCallContext context)
        {
            var response = new GrpcApplicationUserModel();
            ApplicationUser user = _repository.GetUserById(request.Id).GetAwaiter().GetResult();
            response = _mapper.Map<GrpcApplicationUserModel>(user);
            return Task.FromResult(response);
        }


        /* public override Task<ApplicactionUserResponse> GetStudents(GetStudentRequest request, ServerCallContext context)
         {
             var response = new ApplicactionUserResponse();
             var students = _repository.GetUsersByRole("STUDENT");
             response = _mapper.Map<ApplicactionUserResponse>(students);
             return Task.FromResult(response);
         }

         public override Task<ApplicactionUserResponse> GetTeachers(GetTeacherRequest request, ServerCallContext context)
         {
             var response = new ApplicactionUserResponse();
            *//* GrpcApplicationUserModel user = new GrpcApplicationUserModel()
             {

             }*//*

             var teachers = _repository.GetUsersByRole("TEACHER");
             foreach (var teacher in teachers) response.Users.Add(_mapper.Map<GrpcApplicationUserModel>(teacher));
             return Task.FromResult(response);
     }*/
    }
}
