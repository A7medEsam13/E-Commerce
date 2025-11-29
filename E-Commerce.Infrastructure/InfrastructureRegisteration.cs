using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Services;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Repositries;
using E_Commerce.Infrastructure.Repositries.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;



namespace E_Commerce.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositry<>));

            // Apply unit of work pattern.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                )
            );
            // Apply DbContext registration.
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
