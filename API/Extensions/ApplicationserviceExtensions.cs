using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
using API.Services;

namespace API.Extensions
{
    public static class ApplicationserviceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<UserService>();
            services.AddScoped<ArticleService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAdminTokenService, AdminTokenService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));



            return services;

        }
    }
}