using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Username).IsRequired();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(c => c.Email).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();
            builder.Property(c => c.Password).IsRequired().HasMaxLength(30);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.Address).IsRequired().HasMaxLength(30);

            builder.HasMany(c => c.Orders)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
