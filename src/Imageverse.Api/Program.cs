using Imageverse.Api.Common.Aspects;
using Imageverse.Application;
using Imageverse.Infrastructure;
using Prometheus;

namespace Imageverse.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services
                    .AddPresentation()
                    .AddAplication()
                    .AddInfrastructure(builder.Configuration);
            }

            var app = builder.Build();
            {
                app.UseMetricsAllMiddleware();
                app.UseAuthentication();
                app.UseAuthorization();
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger()
                       .UseSwaggerUI();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                }
                app.UseHttpsRedirection();
                app.UseHttpMetrics();
                app.MapMetrics();
                app.MapControllers();
                app.Run();
            }
        }
    }
}