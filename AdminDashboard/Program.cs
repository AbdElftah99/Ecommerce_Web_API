using Persistence.Identity;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Stripe;

namespace AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Infra
            builder.Services.AddDbContext<StoreDbContext>(options =>
                   options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
              options.UseSqlServer(
              builder.Configuration.GetConnectionString("IdentityConnection")));


            builder.Services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                }).AddEntityFrameworkStores<StoreIdentityDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
