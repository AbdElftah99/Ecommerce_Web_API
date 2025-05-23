using Domain.Entities.OrderModules;

namespace Persistence.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(order => order.Product,
                            Product => Product.WithOwner());

            builder.Property(order => order.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
