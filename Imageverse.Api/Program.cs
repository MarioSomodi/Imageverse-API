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
                builder.Services.AddControllers();
                builder.Services.AddSwaggerGen();
                builder.Services
                    .AddAplication()
                    .AddInfrastructure();
            }

            var app = builder.Build();
            {
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