using MMLib.SwaggerForOcelot.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);
var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(opt =>
{
    opt.Folder = routes;
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", build => 
        build.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration).AddPolly();
// builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();



var app = builder.Build();
app.UseCors("AllowAll");

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
    opt.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

    
}).UseOcelot().Wait();
app.UseHttpsRedirection();




app.Run();

public static class AlterUpstream
{
    public static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
    {
        var swagger = JObject.Parse(swaggerJson);
        // ... alter upstream json
        return swagger.ToString(Formatting.Indented);
    }
}

