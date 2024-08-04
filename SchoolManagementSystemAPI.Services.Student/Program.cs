using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Student;
using SchoolManagementSystemAPI.Services.Student.Extension;
using SchoolManagementSystemAPI.Services.Student.Extentions;
using SchoolManagementSystemAPI.Services.Student.Repositories;
using SchoolManagementSystemAPI.Services.Student.Repositories.Data;
using SchoolManagementSystemAPI.Services.Student.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Student.Services;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;
using SchoolManagementSystemAPI.Services.Student.Utils.RabbitMqBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MapperConfig.registerMap().CreateMapper();
builder.Services.AddSingleton(mapper);

var dbOption = new DbContextOptionsBuilder<AppDbContext>();
dbOption.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new StuRegService(dbOption.Options, mapper));
builder.Services.AddHostedService<RabbitMQConsumer>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGrpcApplicationUserClientService, GrpcApplicationUserClientService>();
builder.Services.AddScoped<IGrpcGradeSubjectClient, GrpcGradeSubjectClient>();
builder.Services.AddScoped<IGrpcWebinarClientService, GrpcWebinarClientService>();
builder.AddAuthentication();
builder.AddSwaggerAuth();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//from extension 
builder.AddClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    // app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();

app.UseAuthorization();
ApplyMigration();
app.MapControllers();

app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
