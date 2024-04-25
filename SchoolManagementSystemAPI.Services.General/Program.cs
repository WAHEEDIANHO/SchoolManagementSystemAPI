using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General;
using SchoolManagementSystemAPI.Services.General.GrpcController;
using SchoolManagementSystemAPI.Services.General.Repositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Services;
using SchoolManagementSystemAPI.Services.General.Services.IService;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<AttendanceGrpcController>();
app.MapGrpcService<GradeSubjectGrpcController>();
app.MapGrpcService<GradeGrpcController>();
app.MapGrpcService<SessionGrpcController>();
app.MapGrpcService<SubjectGrpcController>();
app.MapGrpcService<NotificationGrpcController>();
app.MapGrpcService<WebinarGrpcController>();
ApplyMigration();
app.Run();


void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}
