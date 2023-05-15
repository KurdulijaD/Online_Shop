using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Address).IsRequired();

            builder.HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
