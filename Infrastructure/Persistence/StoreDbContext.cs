﻿using Domain.Entities.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        // Products
        public DbSet<Product>       Products { get; set; }
        public DbSet<ProductBrand>  ProductBrands { get; set; }
        public DbSet<ProductType>   ProductTypes { get; set; }

        // Orders
        public DbSet<Order>             Orders { get; set; }
        public DbSet<OrderItem>        OrderItems { get; set; }
        public DbSet<DeliveryMethod>    DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
    }
}
