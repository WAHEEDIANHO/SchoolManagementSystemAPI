using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General;
using SchoolManagementSystemAPI.Services.General.Extension;
using SchoolManagementSystemAPI.Services.General.GrpcController;
using SchoolManagementSystemAPI.Services.General.Repositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Services;
using SchoolManagementSystemAPI.Services.General.Services.IService;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderServicesExtention();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseCors("AllowAll");
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
app.MapGrpcService<AssessmentGrpcController>();
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
