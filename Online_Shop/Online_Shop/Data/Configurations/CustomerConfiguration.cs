using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
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

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerUsername)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
