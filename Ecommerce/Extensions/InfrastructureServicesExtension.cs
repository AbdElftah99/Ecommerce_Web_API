using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Identity;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace Ecommerce.Extensions
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
                    options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StoreIdentityDbContext>(options =>
              options.UseSqlServer(
              configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IBusketRepository, BusketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            // In Memory Database open connection as long as the life time
            services.AddSingleton<IConnectionMultiplexer>(
                 _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

            services.AddIdentityConfiguations();
            services.AddAuthorization();
            services.AddJwtConfiguration(configuration);

            return services;
        }

        public static IServiceCollection AddIdentityConfiguations(this IServiceCollection service)
        {
            service.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<StoreIdentityDbContext>();

            return service;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection service, IConfiguration configs)
        {
            var jwtOptions = configs.GetSection("JWTOptions").Get<JwtOptions>();

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

            return service;
        }
    }
}
