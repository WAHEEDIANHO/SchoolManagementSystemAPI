using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Parent.Extension;
using SchoolManagementSystemAPI.Services.Parent.Repositories;
using SchoolManagementSystemAPI.Services.Parent.Repositories.Data;
using SchoolManagementSystemAPI.Services.Parent.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.Parent.Services;
using SchoolManagementSystemAPI.Services.Parent.Services.IServices;
using SchoolManagementSystemAPI.Services.Parent.Utils.RabbitMQ;
using SchoolManagementSystemAPI.Services.Student;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var dbOptions = new DbContextOptionsBuilder();
dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
IMapper mapper = MapperConfig.registerMap().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton(new ParentRegService(dbOptions.Options, mapper));
builder.Services.AddSingleton<RabbitMQBusConsumer>();

builder.Services.AddHostedService<RabbitMQBusConsumer>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//self
builder.AddClient();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParentService, ParentService>();

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

app.Run();
