using AutoMapper;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<SubjectRequestDTO, SubjectResponseDTO>().ReverseMap();
                config.CreateMap<Subject, SubjectResponseDTO>().ReverseMap();
                config.CreateMap<Subject, SubjectRequestDTO>().ReverseMap();
                config.CreateMap<Grade, GradeDTO>().ReverseMap();
                config.CreateMap<Session, SessionDTO>().ReverseMap();
                config.CreateMap<ClassSubject, ClassSubjectDTO>().ReverseMap();
            });

            return mappingConfiguration;
        }
    }
}
