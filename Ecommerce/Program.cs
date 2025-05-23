using Domain.Contracts;
using Ecommerce.Extensions;
using Ecommerce.Factory;
using Ecommerce.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using Services.Abstraction;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCoreServices(builder.Configuration)
                            .AddInfrastructreServices(builder.Configuration)
                            .AddPersentationServices();

            var app = builder.Build();

            // Subscribe to our global Exception Handler
            app.UseCustomExceptionMiddleware();

            // Use Static files like images, css, js
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            await app.SeedDbAsync();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
