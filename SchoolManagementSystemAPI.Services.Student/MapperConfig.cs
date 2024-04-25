using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.Student.Model.Dto;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Repositories;

namespace SchoolManagementSystemAPI.Services.Student
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<StudentSchema, MsgRegStudentDTO>().ReverseMap();
                config.CreateMap<UserResponseDTO, StudentDTO>().ReverseMap();
                config.CreateMap<StudentSchema , StudentDTO>()
                .ForMember(dest => dest.Id, u => u.MapFrom(src => src.RegId))
                .ForMember(dest => dest.SessionId, u => u.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.ClassId, u => u.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.AdmissionNo, u => u.MapFrom(src => src.AdmissionNo));
                config.CreateMap<UserResponseDTO, GrpcApplicationUserModel>().ReverseMap();
                config.CreateMap<ClassSubjectDTO, GradeSubjectDataGrpc>().ReverseMap();
                config.CreateMap<TopicDTO, TopicGrpc>().ReverseMap();
                config.CreateMap<LessonDTO, LessonGrpc>().ReverseMap();
                config.CreateMap<Webinar, WebinargRPC>()
                    .ForMember(dest => dest.WebinarDate, u => u.MapFrom(src => src.WebinarDate.ToString("yyyy-MM-dd")))
                    .ReverseMap()
                    .ForMember(dest => dest.WebinarDate, u => u.MapFrom(src => !string.IsNullOrEmpty(src.WebinarDate) ? DateTime.Parse(src.WebinarDate) : DateTime.MinValue));
                config.CreateMap<TopicWebinarGrpc, TopicDTO>().ReverseMap();

            });

            return mappingConfiguration;
        }
    }
}
