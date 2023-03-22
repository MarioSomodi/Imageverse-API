using Imageverse.Api.Common.Errors;
using Imageverse.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Imageverse.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddMappings();
            
            services.AddSingleton<ProblemDetailsFactory, ImageverseProblemDetailsFactory>();

            return services;
        }
    }
}
