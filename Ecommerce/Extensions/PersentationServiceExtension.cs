using Ecommerce.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Presentation;

namespace Ecommerce.Extensions
{
    public static class PersentationServiceExtension
    {
        public static IServiceCollection AddPersentationServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers()
                    .AddApplicationPart(typeof(ApiController).Assembly);

            // Override API Controller to handle Invalid Model State Response
            services.Configure<ApiBehaviorOptions>
                (
                    options => options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationError
                );


            AddSwaggerConfiguration(services);

            services.AddCors(config =>
            {
                config.AddPolicy("policy", options =>
                {
                    options.WithOrigins("http://http://localhost:4200", "https://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            return services;

        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In              = ParameterLocation.Header,
                    Description     = "Please Enter you Bearer token",
                    Name            = "Authorization",
                    Type            = SecuritySchemeType.Http,
                    Scheme          = "Bearer",
                    BearerFormat    = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>(){}
                    }
                });
            });

            return services;
        }
    }
}
