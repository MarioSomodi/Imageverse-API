using Imageverse.Api.Common.Errors;
using Imageverse.Application;
using Imageverse.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Imageverse.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddControllers();
                builder.Services.AddSwaggerGen();
                builder.Services.AddSingleton<ProblemDetailsFactory, ImageverseProblemDetailsFactory>();
                builder.Services
                    .AddAplication()
                    .AddInfrastructure(builder.Configuration);
            }

            var app = builder.Build();
            {
                app.UseExceptionHandler("/Error");
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger()
                       .UseSwaggerUI();
                }
                app.UseHttpsRedirection();
                app.MapControllers();
                app.Run();
            }
        }
    }
}