using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Online_Shop.Common;
using Online_Shop.Data;

namespace Online_Shop.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Online_Shop.Models.Admin", b =>
            {
                b.Property<string>("Username")
                    .HasColumnType("string")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime");

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(450)");

                b.Property<byte[]>("Image")
                    .HasColumnType("varbinary(max)");

                b.Property<string>("Name")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<string>("Password")
                    .HasMaxLength(450)
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Username");

                b.HasIndex("Email")
                    .IsUnique()
                    .HasFilter("[Email] IS NOT NULL");

                b.HasIndex("Username")
                    .IsUnique()
                    .HasFilter("[Username] IS NOT NULL");

                b.ToTable("Admins", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.Customer", b =>
            {
                b.Property<string>("Username")
                    .HasColumnType("string")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime");

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(450)");

                b.Property<byte[]>("Image")
                    .HasColumnType("varbinary(max)");

                b.Property<string>("Name")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<string>("Password")
                    .HasMaxLength(450)
                    .HasColumnType("nvarchar(450)");

                b.Property<bool>("Status")
                    .HasColumnType("bit");

                b.HasKey("Username");

                b.HasIndex("Email")
                    .IsUnique()
                    .HasFilter("[Email] IS NOT NULL");

                b.HasIndex("Username")
                    .IsUnique()
                    .HasFilter("[Username] IS NOT NULL");

                b.ToTable("Customers", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.Salesman", b =>
            {
                b.Property<string>("Username")
                    .HasColumnType("string")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Address")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime");

                b.Property<string>("Email")
                    .HasColumnType("nvarchar(450)");

                b.Property<byte[]>("Image")
                    .HasColumnType("varbinary(max)");

                b.Property<string>("Name")
                    .HasMaxLength(30)
                    .HasColumnType("nvarchar(30)");

                b.Property<string>("Password")
                    .HasMaxLength(450)
                    .HasColumnType("nvarchar(450)");

                b.Property<bool>("Status")
                    .HasColumnType("bit");

                b.Property<EVerificationStatus>("Verified")
                    .HasColumnType("int");

                b.HasKey("Username");

                b.HasIndex("Email")
                    .IsUnique()
                    .HasFilter("[Email] IS NOT NULL");

                b.HasIndex("Username")
                    .IsUnique()
                    .HasFilter("[Username] IS NOT NULL");

                b.ToTable("Salesmans", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("CustomerUsername")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Address")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Comment")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DeliveryTime")
                    .HasColumnType("datetime");

                b.Property<DateTime>("OrderTime")
                    .HasColumnType("datetime");

                b.Property<EOrderStatus>("Status")
                    .HasColumnType("int");

                b.Property<double>("Price")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.HasIndex("CustomerUsername");

                b.ToTable("Orders", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<double>("Price")
                    .HasColumnType("float");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("SalesmanUsername")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Amount")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("Deleted")
                    .HasColumnType("bit");

                b.Property<byte[]>("Image")
                    .HasColumnType("varbinary(max)");

                b.HasKey("Id");

                b.HasIndex("SalesmanUsername");

                b.ToTable("Products", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.OrderProduct", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("Amount")
                    .HasColumnType("int");

                b.Property<int>("OrderId")
                    .HasColumnType("int");

                b.Property<int>("ProductId")
                   .HasColumnType("int");

                b.Property<double>("Price")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.HasIndex("OrderId");

                b.HasIndex("ProductId");

                b.ToTable("OrderProducts", "dbo");
            });

            modelBuilder.Entity("Online_Shop.Models.Admin", b =>
            {
                b.HasMany("Online_Shop.Models.Salesman", "Salesman")
                    .WithOne("Admin")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasMany("Online_Shop.Models.Customer", "Customer")
                    .WithOne("Admin")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Admin");
            });

            modelBuilder.Entity("Online_Shop.Models.Order", b =>
            {
                b.HasOne("Online_Shop.Models.Customer", "Customer")
                    .WithMany("Orders")
                    .HasForeignKey("CustomerUsername")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

            modelBuilder.Entity("Online_Shop.Models.Order", b =>
            {
                b.HasMany("Online_Shop.Models.OrderProduct", "OrderProduct")
                    .WithOne("Order")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("OrderProduct");
            });

            modelBuilder.Entity("Online_Shop.Models.Order", b =>
            {
                b.Navigation("Products");
            });

#pragma warning restore 612, 618
        }
    }
}
