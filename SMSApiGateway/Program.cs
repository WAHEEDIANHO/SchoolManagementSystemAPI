using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// include setup to read ocelot configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(("AllowAll"),
        build => build.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json");
// builder.Services.AddSwaggerForOcelot(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI  at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerForOcelotUI(opt =>
    // {
    //     opt.PathToSwaggerGenerator = "/swagger/docs";
    //
    // });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.Run();
