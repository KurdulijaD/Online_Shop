using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Username).IsRequired();
            builder.HasIndex(a => a.Username).IsUnique();
            builder.Property(a => a.Email).IsRequired();
            builder.HasIndex(a => a.Email).IsUnique();
            builder.Property(a => a.Password).IsRequired().HasMaxLength(30);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(30);
            builder.Property(a => a.BirthDate).IsRequired();
            builder.Property(a => a.Address).IsRequired().HasMaxLength(30);

            builder.HasMany(a => a.Salesmens)
            .WithOne(a => a.Admin)
            .HasForeignKey(s => s.AdminId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction); 

            builder.HasMany(a => a.Customers)
                .WithOne(a => a.Admin)
                .HasForeignKey(c => c.AdminId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
