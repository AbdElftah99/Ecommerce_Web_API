using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                    .WithMany()
                    .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                    .WithMany()
                    .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
