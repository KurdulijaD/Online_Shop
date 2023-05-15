using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.HasMany(s => s.Products)
                .WithOne(p => p.Salesman)
                .HasForeignKey(p => p.SalesmanUsername)
                .IsRequired();

            builder.HasMany(s => s.Orders)
                .WithOne(o => o.Salesman);
        }
    }
}
