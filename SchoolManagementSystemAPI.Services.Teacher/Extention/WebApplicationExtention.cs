using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SchoolManagementSystemAPI.Services.Teacher.Extention
{
    public static class WebApplicationExtention
    {
        public static WebApplicationBuilder AddClient(this WebApplicationBuilder builder) {
            builder.Services.AddHttpClient("UserAPI", opt => opt.BaseAddress = new Uri(builder.Configuration["ServiceUrl:UserAPI"]));
            return builder;
        } 

        public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
        {

            var secret = builder.Configuration.GetValue<string>("ApiSetting:Secret");
            var audience = builder.Configuration.GetValue<string>("ApiSetting:Audience");
            var issuer = builder.Configuration.GetValue<string>("ApiSetting:Issuer");

            var key = Encoding.ASCII.GetBytes(secret);
            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidAudience = audience,
                    ValidIssuer = issuer
                };
            });
            return builder;
        }

        public static WebApplicationBuilder AddSwaggerAuthenticationInput(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[]{}
                    }
                });
            });


            return builder;
        }
    }
}
