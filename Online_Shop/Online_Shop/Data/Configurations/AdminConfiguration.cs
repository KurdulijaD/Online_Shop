using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasMany(a => a.Salesmens)
            .WithOne(a => a.Admin)
            .HasForeignKey(s => s.AdminUsername)
            .IsRequired();

            builder.HasMany(a => a.Customers)
                .WithOne(a => a.Admin)
                .HasForeignKey(c => c.AdminUsername)
                .IsRequired();
        }
    }
}
