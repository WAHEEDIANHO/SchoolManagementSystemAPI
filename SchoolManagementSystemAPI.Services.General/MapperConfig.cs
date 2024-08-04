using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General
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
                config.CreateMap<GradeSubject, ClassSubjectDTO>().ReverseMap();
                config.CreateMap<AttendanceHeader, CreateAttendanceSheet>().ReverseMap();
                config.CreateMap<AttendanceHeader, AttendanceHeaderDTO>().ReverseMap();
                config.CreateMap<AttendanceDetail, AttendanceDetailReqDTO>().ReverseMap();
                config.CreateMap<AttendanceDetail, AttendanceDetailDTO>().ReverseMap();
                config.CreateMap<CreateAttendanceSheet, AttendanceHeaderReq>().ReverseMap();
                config.CreateMap<AttendanceHeaderGrpc, AttendanceHeaderDTO>().ReverseMap();
                config.CreateMap<CreateAttendanceSheet, AttendanceHeaderReq>().ReverseMap();
                config.CreateMap<AttendanceDetailGrpc, AttendanceDetailDTO>().ReverseMap();
                config.CreateMap<AttendanceDetailReq, AttendanceDetailReqDTO>().ReverseMap();
                config.CreateMap<GradeDTO, GradeDataGrpc>().ReverseMap();
                config.CreateMap<SessionDTO, SessionGrpcData>().ReverseMap();
                config.CreateMap<ClassSubjectDTO, GradeSubjectDataGrpc>().ReverseMap();
                config.CreateMap<SubjectRequestDTO, SubjectDataGrpc>().ReverseMap();
                config.CreateMap<SubjectResponseDTO, SubjectDataGrpc>().ReverseMap();
                config.CreateMap<NotificationDTO, Notification>().ReverseMap();
                config.CreateMap<NotificationDTO, NotificationGrpcReqData>().ReverseMap();
                config.CreateMap<NotificationRespDTO, Notification>().ReverseMap();
                config.CreateMap<NotificationGrpcData, NotificationRespDTO>()
                    .ForMember(dest => dest.Created, u => u.MapFrom(src => DateTime.Parse(src.Created))).ReverseMap();
                config.CreateMap<Lesson, LessonDTO>().ReverseMap();
                config.CreateMap<Lesson, LessonReqDTO>().ReverseMap();
                config.CreateMap<Topic, TopicReqDTO>()
                    .ForMember(dest => dest.GradeNumber, u => u.MapFrom(src => src.GradeSubjectGradeNumber))
                    .ForMember(dest => dest.SubjectTitle, u => u.MapFrom(src => src.GradeSubjectSubjectTitle))
                    .ForMember(dest => dest.TermNumber, u => u.MapFrom(src => src.TermTermNumber))
                    .ForMember(dest => dest.SessionName, u => u.MapFrom(src => src.TermSessionName)).ReverseMap();
                config.CreateMap<Topic, TopicDTO>()
                    .ForMember(dest => dest.GradeNumber, u => u.MapFrom(src => src.GradeSubjectGradeNumber))
                    .ForMember(dest => dest.SubjectTitle, u => u.MapFrom(src => src.GradeSubjectSubjectTitle)).ReverseMap();
                config.CreateMap<Event, EventDTO>().ReverseMap();
                config.CreateMap<Event, EventReqDTO>().ReverseMap();
                config.CreateMap<TopicGrpc, TopicDTO>().ReverseMap();
                config.CreateMap<LessonGrpc, LessonDTO>().ReverseMap();
                config.CreateMap<WebinarReqGrpcDto, WebinarReqDto>()
                    .ForMember(dest => dest.WebinarDate, u => u.MapFrom(src => DateTime.Parse(src.WebinarDate))).ReverseMap();
                config.CreateMap<Webinar, WebinargRPC>()
                    .ForMember(dest => dest.WebinarDate, u => u.MapFrom(src => src.WebinarDate.ToString("yyyy-MM-dd")))
                    .ReverseMap();
                config.CreateMap<Webinar, WebinarReqDto>().ReverseMap();
                config.CreateMap<TopicWebinarGrpc, Topic>()
                    .ForMember(dest => dest.GradeSubjectGradeNumber, u => u.MapFrom(src => src.GradeNumber))
                    .ForMember(dest => dest.GradeSubjectSubjectTitle, u => u.MapFrom(src => src.SubjectTitle))
                    .ReverseMap();
                config.CreateMap<AssessmentGrpc, Assessment>() //<src, dest>
                    .ForMember(dest => dest.DateSchedule, u => u.MapFrom(src => DateTime.Parse(src.DateSchedule)))
                    .ReverseMap();
                config.CreateMap<AssessmentDtoGrpc, Assessment>()
                    .ForMember(dest => dest.DateSchedule, u => u.MapFrom(src => DateTime.Parse(src.DateSchedule)))
                    .ReverseMap();
                config.CreateMap<TermDto, Term>().ReverseMap();
                config.CreateMap<NoteDto, Note>().ReverseMap();
                config.CreateMap<NoteResDto, Note>().ReverseMap();
                config.CreateMap<Discussion, DiscussionReqDto>().ReverseMap();
                config.CreateMap<DiscussionDtoGrpc, DiscussionReqDto>()
                .ForMember(dest => dest.DissusionDateTime, u => u.MapFrom(src => DateTime.Parse(src.DiscussionDateTime))).ReverseMap();;
                // .ForMember(dest => dest.TermStartDate, u => u.MapFrom(src => DateTime.Parse(src.TermStartDate)))
            });

            return mappingConfiguration;
        }
    }
}
