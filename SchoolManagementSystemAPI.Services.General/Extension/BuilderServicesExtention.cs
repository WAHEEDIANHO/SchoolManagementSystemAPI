using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Services;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Extension;

public static class BuilderServicesExtention
{
    public static WebApplicationBuilder AddBuilderServicesExtention(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opt =>
        {
           opt.AddPolicy("AllowAll", build =>
           {
              build.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
           }); 
        });
        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        IMapper mapper = MapperConfig.registerMap().CreateMapper();
        builder.Services.AddSingleton(mapper);
        builder.Services.AddGrpc();

        builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
        builder.Services.AddScoped<ISubjectServices,  SubjectService>();

        builder.Services.AddScoped<IGradeRepository, GradeRepository>();
        builder.Services.AddScoped<IGradeService, GradeService>();

        builder.Services.AddScoped<ISessionRepository, SessionRepository>();
        builder.Services.AddScoped<ISessionService, SessionService>();

        builder.Services.AddScoped<IClassSubjectRepository, ClassSubjectRepository>();
        builder.Services.AddScoped<IClassSubjectService, ClassSubjectService>();

        builder.Services.AddScoped<IAttendanceHeaderRepository, AttendanceHeaderRepository>();
        builder.Services.AddScoped<IAttendanceDetailRepository, AttendanceDetailRepository>();
        builder.Services.AddScoped<IAttendanceService, AttendanceService>();

        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationService, NotificationService>();

        // Topic
        builder.Services.AddScoped<ITopicRepository, TopicRepository>();
        builder.Services.AddScoped<ITopicService, TopicService>();

        //Lessom
        builder.Services.AddScoped<ILessonRepository, LessonRepository>();

        //Event
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IEventService, EventService>();

        //Webinar
        builder.Services.AddScoped<IWebinarRepository, WebinarRepository>();
        builder.Services.AddScoped<IWebinarService, WebinarService>();

        //Assessment
        builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
        builder.Services.AddScoped<IAssessmentService, AssessmentService>();
        
        // Session terms
        builder.Services.AddScoped<ITermRepository, TermRepository>();
        builder.Services.AddScoped<ITermService, TermService>();
        
        //Note
        builder.Services.AddScoped<INoteRepository, NoteRepository>();
        builder.Services.AddScoped<INoteService, NoteService>();
        
        //Discussion
        builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
        builder.Services.AddScoped<IDiscussionService, DiscussionService>();
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Cloudinary
        builder.Services.AddSingleton<CloudinaryService>();


        return builder;
    }
}