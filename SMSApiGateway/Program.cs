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
builder.Services.AddOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json");
// Learn more about configuring Swagger/OpenAPI  at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.Run();
