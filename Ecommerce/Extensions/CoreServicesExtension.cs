using Services.Abstraction;
using Services;
using Shared;

namespace Ecommerce.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(ServicesAssembly).Assembly);

            services.Configure<JwtOptions>(config.GetSection("JWTOptions"));

            return services;
        }
    }
}
