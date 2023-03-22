using Imageverse.Application;
using Imageverse.Infrastructure;

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