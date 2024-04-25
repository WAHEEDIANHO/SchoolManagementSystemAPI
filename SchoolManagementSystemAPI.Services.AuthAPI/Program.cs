using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.AuthAPI;
using SchoolManagementSystemAPI.Services.AuthAPI.Model;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.Data;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Services;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;
using Hangfire;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.HangFireServiceManagement;
using SchoolManagementSystemAPI.Services.AuthAPI.Utils.RabbitMQ;
using SchoolManagementSystemAPI.Services.AuthAPI.Utils.Grpc;
//using SchoolManagementSystemAPI.Services.AuthAPI.Behaviour;
using SchoolManagementSystemAPI.Services.AuthAPI.Idempotency;
using SchoolManagementSystemAPI.Services.AuthAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Step 1: Add CORS services to the DI container
builder.Services.AddCors(options =>
{
    // Step 2: Configure CORS policy to allow all origins, headers, and methods
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Rest of your code...
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MapperConfig.registerMap().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSetting:JwtOptions"));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService<RegisterRequestDTO>, AuthService<RegisterRequestDTO>>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var dbOption = new DbContextOptionsBuilder<AppDbContext>();
dbOption.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new UserDeleteService(dbOption.Options, mapper));
builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddGrpc();
builder.Services.AddScoped<IIdenpotencyServices, IdempotencyService>();
builder.AddAuthentication();
builder.AddSwaggerAuth();
/*builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(IdempotentCommandPipelineBehaviour<,>));
});*/


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering hangfire
builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
    //.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings();
});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<IHangFireServiceManagement, HangfireFireServiceManagement>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IHangFireServiceManagement>(x => x.CheckSentMessageUpdate(), "0 * * ? * *");

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcApplicationUserService>();
app.MapGet("/prorto/user.proto", async context =>
{
    context.Response.WriteAsync(File.ReadAllText("Protos/user.proto"));
});
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
