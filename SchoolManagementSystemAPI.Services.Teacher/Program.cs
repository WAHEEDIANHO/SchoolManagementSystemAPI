using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Teacher;
using SchoolManagementSystemAPI.Services.Teacher.Extention;
using SchoolManagementSystemAPI.Services.Teacher.Repositories;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.Data;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Teacher.Services;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;
using SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService;
using SchoolManagementSystemAPI.Services.Teacher.Utils.RabbitMQBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

IMapper mapper = MapperConfig.registerMap().CreateMapper();
builder.Services.AddSingleton(mapper);

var dbOptions = new DbContextOptionsBuilder();
dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new TeacherRegService(dbOptions.Options, mapper));
builder.Services.AddHostedService<RabbitMQBusConsumer>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGrpcApplicationUserClientService, GrpcApplicationUserClientService>();
builder.AddClient();
builder.AddAuthentication();
builder.AddSwaggerAuthenticationInput();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
Console.WriteLine($"------> db uri = {app.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")}");
ApplyMigration();
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
