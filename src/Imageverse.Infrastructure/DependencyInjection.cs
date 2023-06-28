using Amazon.S3;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Infrastructure.Authentication;
using Imageverse.Infrastructure.Persistance;
using Imageverse.Infrastructure.Persistance.Interceptors;
using Imageverse.Infrastructure.Persistance.Repositories;
using Imageverse.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Imageverse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistance(configuration)
                .AddAWS(configuration)
                .AddCustomServices();


            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<PublishDomainEventsInterceptor>();

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
				.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IHashtagRepository, HashtagRepository>()
                .AddScoped<IUserLimitRepository, UserLimitRepository>()
                .AddScoped<IUserActionLogRepository, UserActionLogRepository>()
				.AddScoped<IPostRepository, PostRepository>()
				.AddScoped<IUserActionRepository, UserActionRepository>()
				.AddScoped<IPackageRepository, PackageRepository>();

			services.AddDbContext<ImageverseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ImageverseDB")));

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<IDatabaseLogger, DatabaseLogger>();

            return services;
        }

        public static IServiceCollection AddAWS(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IAWSHelper, AWSHelper>();
            
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            JwtSettings jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddHttpContextAccessor();
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        // Set to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    });

            return services;
        }
    }
}