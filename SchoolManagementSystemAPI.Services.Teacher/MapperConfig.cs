using AutoMapper;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;
using SchoolManagementSystemAPI.Services.TeacherAPI;

namespace SchoolManagementSystemAPI.Services.Teacher
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<TeacherSchema, MsgRegTeacherDTO>().ReverseMap();
                config.CreateMap<UserResponseDTO, GrpcApplicationUserModel>().ReverseMap();
            });

            return mappingConfiguration;
        }
    }
}
