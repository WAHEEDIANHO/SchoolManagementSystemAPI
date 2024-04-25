using AutoMapper;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
//using SchoolManagementSystemAPI.Services.AuthAPI.Model.Dto;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;

namespace SchoolManagementSystemAPI.Services.AuthAPI
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUser, RegisterRequestDTO>().ReverseMap();
                config.CreateMap<ApplicationUser, StudentRegisterDTO>().ReverseMap();
                config.CreateMap<ApplicationUser, TeacherRegisterDTO>().ReverseMap();
                config.CreateMap<ApplicationUser, ParentRegistrationDTO>().ReverseMap();
                config.CreateMap<MsgRegStudentDTO, StudentRegisterDTO>().ReverseMap();
                config.CreateMap<UserResponseDTO, ApplicationUser>().ReverseMap();
                //config.CreateMap<UserResponseDTO, GrpcApplicationUserModel>().ReverseMap();
                config.CreateMap<GrpcApplicationUserModel, ApplicationUser>().ReverseMap();
            })
            {

            };
            return mappingConfiguration;
        }
    }
}
