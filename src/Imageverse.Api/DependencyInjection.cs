using Imageverse.Api.Common.Errors;
using Imageverse.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Imageverse.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();

            services.AddMappings();
            
            services.AddSingleton<ProblemDetailsFactory, ImageverseProblemDetailsFactory>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            string? versionOfAPI = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Imageverse API", Version = versionOfAPI ?? "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}
