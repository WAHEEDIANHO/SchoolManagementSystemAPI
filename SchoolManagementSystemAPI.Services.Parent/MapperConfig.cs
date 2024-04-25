using AutoMapper;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Repositories;

namespace SchoolManagementSystemAPI.Services.Parent
{
    public class MapperConfig
    {
        public static MapperConfiguration registerMap()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<ParentSchema, MsgRegParentDTO>().ReverseMap();
                config.CreateMap<UserResponseDTO, GrpcApplicationUserModel>().ReverseMap();
            })
            {

            };
            return mappingConfiguration;
        }
    }
}
