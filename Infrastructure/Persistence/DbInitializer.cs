using Domain.Entities.Identity;
using Domain.Entities.OrderModules;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer
        (StoreDbContext context,
        UserManager<User> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext identityDbContext) 
        : IDbInitializer
    {
        public async Task InitializeAsync()
        {
			try
			{
                // Check if database exist
                if (context.Database.GetPendingMigrations().Any())
                    await context.Database.MigrateAsync();

                // Apply seeding    
                if (!context.ProductBrands.Any())
                {
                    var jsonData    = await File.ReadAllTextAsync("../Infrastructure/Persistence/Data/Seeding/brands.json");
                    var brands      = JsonSerializer.Deserialize<List<ProductBrand>>(jsonData);

                    if (brands != null && brands.Any())
                    {
                        await context.ProductBrands.AddRangeAsync(brands);
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.ProductTypes.Any())
                {
                    var jsonData    = await File.ReadAllTextAsync("../Infrastructure/Persistence/Data/Seeding/types.json");
                    var types      = JsonSerializer.Deserialize<List<ProductType>>(jsonData);

                    if (types != null && types.Any())
                    {
                        await context.ProductTypes.AddRangeAsync(types);
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Products.Any())
                {
                    var jsonData = await File.ReadAllTextAsync("../Infrastructure/Persistence/Data/Seeding/products.json");
                    var broducts = JsonSerializer.Deserialize<List<Product>>(jsonData);

                    if (broducts != null && broducts.Any())
                    {
                        await context.Products.AddRangeAsync(broducts);
                        await context.SaveChangesAsync();
                    }
                }

                await SeedDeliveryMethods(context);
            }
			catch (Exception)
			{

				throw;
			}
        }

        public async Task InitializeIdentityAsync()
        {
            // Check if database exist
            if (identityDbContext.Database.GetPendingMigrations().Any())
                await identityDbContext.Database.MigrateAsync();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!_userManager.Users.Any())
            {
                var SuperUserAdmin = new User
                {
                    Email = "SuperAdmin@gmail.com",
                    Description = "Super Admin",
                    UserName = "SuperAdmin",
                    PhoneNumber = "1234567890",
                };
                var userAdmin = new User
                {
                    Email = "Admin@gmail.com",
                    Description = " Admin",
                    UserName = "Admin",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(SuperUserAdmin, "Test@123");
                await _userManager.CreateAsync(userAdmin, "Test@123");

                await _userManager.AddToRoleAsync(SuperUserAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(userAdmin, "Admin");
            }
        }

        private async Task SeedDeliveryMethods(StoreDbContext context)
        {
            // Apply seeding    
            if (!context.DeliveryMethods.Any())
            {
                var jsonData = await File.ReadAllTextAsync("../Infrastructure/Persistence/Data/Seeding/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(jsonData);

                if (deliveryMethods != null && deliveryMethods.Any())
                {
                    await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
