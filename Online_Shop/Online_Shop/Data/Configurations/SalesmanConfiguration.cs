using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Username).IsRequired();
            builder.HasIndex(s => s.Username).IsUnique();
            builder.Property(s => s.Email).IsRequired();
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.Password).IsRequired().HasMaxLength(30);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(30);
            builder.Property(s => s.BirthDate).IsRequired();
            builder.Property(s => s.Address).IsRequired().HasMaxLength(30);

            builder.HasMany(s => s.Products)
                .WithOne(s => s.Salesman)
                .HasForeignKey(s => s.SalesmanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Orders)
                .WithOne(s => s.Salesman);
        }
    }
}
