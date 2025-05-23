using Ecommerce.Middlewares;

namespace Ecommerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public async static Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbInitualizer = services.GetRequiredService<IDbInitializer>();
            await dbInitualizer.InitializeAsync();
            await dbInitualizer.InitializeIdentityAsync();

            return app;
        }

        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
        {
            // Subscribe to our global Exception Handler
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
