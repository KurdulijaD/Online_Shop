using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Username);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password).HasMaxLength(450);
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Address).HasMaxLength(30);
            builder.HasMany(x => x.Salesmens)
                  .WithOne(x => x.Admin);
            builder.HasMany(x => x.Customers)
                  .WithOne(x => x.Admin);
        }
    }
}
