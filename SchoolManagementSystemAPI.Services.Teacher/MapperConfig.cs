using AutoMapper;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;

namespace SchoolManagementSystemAPI.Services.Student
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<TeacherSchema, MsgRegTeacherDTO>().ReverseMap();
            });

            return mappingConfiguration;
        }
    }
}
