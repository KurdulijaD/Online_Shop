using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Username).IsRequired();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.Password).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.Address).IsRequired().HasMaxLength(30);
        }
    }
}
