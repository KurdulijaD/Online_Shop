using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.HasKey(u => u.Username);
            builder.Property(u => u.Username).IsRequired();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Password).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(30);
            builder.Property(u => u.BirthDate).IsRequired();
            builder.Property(u => u.Address).IsRequired().HasMaxLength(30);

            builder.HasMany(s => s.Products)
                .WithOne(p => p.Salesman)
                .HasForeignKey(p => p.SalesmanUsername)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Orders)
                .WithOne(o => o.Salesman);
        }
    }
}
