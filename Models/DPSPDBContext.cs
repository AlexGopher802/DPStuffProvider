using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DPSP_Api
{
    public partial class DPSPDBContext : DbContext
    {
        public DPSPDBContext()
        {
        }

        public DPSPDBContext(DbContextOptions<DPSPDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressDelivery> AddressDeliveries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientAddress> ClientAddresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<OrderFinished> OrderFinisheds { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Ordered> Ordereds { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductCompos> ProductCompos { get; set; }
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<StoreInfo> StoreInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=194.32.248.98;user id=dpsp-client;password=Dpsp3434!;persistsecurityinfo=True;database=DPSPdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<AddressDelivery>(entity =>
            {
                entity.ToTable("AddressDelivery");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ApartmentNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("apartmentNum");

                entity.Property(e => e.FloorNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("floorNum");

                entity.Property(e => e.FrontDoor)
                    .HasColumnType("int(11)")
                    .HasColumnName("frontDoor");

                entity.Property(e => e.Intercom)
                    .HasMaxLength(20)
                    .HasColumnName("intercom")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasIndex(e => e.IdContacts, "idContacts");

                entity.HasIndex(e => e.IdPersonalInfo, "idPersonalInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdContacts)
                    .HasColumnType("int(11)")
                    .HasColumnName("idContacts");

                entity.Property(e => e.IdPersonalInfo)
                    .HasColumnType("int(11)")
                    .HasColumnName("idPersonalInfo");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdContactsNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdContacts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Client_ibfk_2");

                entity.HasOne(d => d.IdPersonalInfoNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdPersonalInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Client_ibfk_1");
            });

            modelBuilder.Entity<ClientAddress>(entity =>
            {
                entity.ToTable("ClientAddress");

                entity.HasIndex(e => e.IdAddress, "idAddress");

                entity.HasIndex(e => e.IdClient, "idClient");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdAddress)
                    .HasColumnType("int(11)")
                    .HasColumnName("idAddress");

                entity.Property(e => e.IdClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("idClient");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientAddress_ibfk_2");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientAddress_ibfk_1");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.ToTable("Courier");

                entity.HasIndex(e => e.IdContacts, "idContacts");

                entity.HasIndex(e => e.IdPersonalInfo, "idPersonalInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdContacts)
                    .HasColumnType("int(11)")
                    .HasColumnName("idContacts");

                entity.Property(e => e.IdPersonalInfo)
                    .HasColumnType("int(11)")
                    .HasColumnName("idPersonalInfo");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.OrderQuantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderQuantity")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdContactsNavigation)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.IdContacts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Courier_ibfk_2");

                entity.HasOne(d => d.IdPersonalInfoNavigation)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.IdPersonalInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Courier_ibfk_1");
            });

            modelBuilder.Entity<OrderFinished>(entity =>
            {
                entity.ToTable("OrderFinished");

                entity.HasIndex(e => e.IdOrder, "idOrder");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ClientScore)
                    .HasColumnType("int(11)")
                    .HasColumnName("clientScore");

                entity.Property(e => e.Commentary)
                    .HasMaxLength(500)
                    .HasColumnName("commentary")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("idOrder");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderFinisheds)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderFinished_ibfk_1");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Ordered>(entity =>
            {
                entity.ToTable("Ordered");

                entity.HasIndex(e => e.IdAddress, "idAddress");

                entity.HasIndex(e => e.IdClient, "idClient");

                entity.HasIndex(e => e.IdCourier, "idCourier");

                entity.HasIndex(e => e.IdOrderStatus, "idOrderStatus");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CodeToFinish)
                    .HasMaxLength(4)
                    .HasColumnName("codeToFinish")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.Commentary)
                    .HasMaxLength(300)
                    .HasColumnName("commentary")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("date")
                    .HasColumnName("deliveryDate");

                entity.Property(e => e.DeliveryTimeFrom)
                    .HasColumnType("time")
                    .HasColumnName("deliveryTimeFrom");

                entity.Property(e => e.DeliveryTimeTo)
                    .HasColumnType("time")
                    .HasColumnName("deliveryTimeTo");

                entity.Property(e => e.IdAddress)
                    .HasColumnType("int(11)")
                    .HasColumnName("idAddress");

                entity.Property(e => e.IdClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("idClient");

                entity.Property(e => e.IdCourier)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCourier");

                entity.Property(e => e.IdOrderStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("idOrderStatus");

                entity.Property(e => e.OrderDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDateTime");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority")
                    .HasDefaultValueSql("'5'");

                entity.Property(e => e.Summ)
                    .HasColumnName("summ")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_1");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_2");

                entity.HasOne(d => d.IdCourierNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdCourier)
                    .HasConstraintName("Ordered_ibfk_3");

                entity.HasOne(d => d.IdOrderStatusNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdOrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_4");
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.ToTable("PersonalInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.IdCategory, "idCategory");

                entity.HasIndex(e => e.IdStoreInfo, "idStoreInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Avail)
                    .HasColumnName("avail")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.IdCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCategory");

                entity.Property(e => e.IdStoreInfo)
                    .HasColumnType("int(11)")
                    .HasColumnName("idStoreInfo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_ibfk_1");

                entity.HasOne(d => d.IdStoreInfoNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdStoreInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_ibfk_2");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("ProductAttribute");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.HasIndex(e => e.IdParentCategory, "idParentCategory");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdParentCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("idParentCategory");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(300)
                    .HasColumnName("imageUrl")
                    .HasDefaultValueSql("''")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.IdParentCategoryNavigation)
                    .WithMany(p => p.InverseIdParentCategoryNavigation)
                    .HasForeignKey(d => d.IdParentCategory)
                    .HasConstraintName("ProductCategory_ibfk_1");
            });

            modelBuilder.Entity<ProductCompos>(entity =>
            {
                entity.HasIndex(e => e.IdOrder, "idOrder");

                entity.HasIndex(e => e.IdProduct, "idProduct");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("idOrder");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProduct");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Summ).HasColumnName("summ");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ProductCompos)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductCompos_ibfk_2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductCompos)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductCompos_ibfk_1");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription");

                entity.HasIndex(e => e.IdProduct, "idProduct");

                entity.HasIndex(e => e.IdProductAttribute, "idProductAttribute");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AttrValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("attrValue")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProduct");

                entity.Property(e => e.IdProductAttribute)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProductAttribute");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductDescriptions)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductDescription_ibfk_1");

                entity.HasOne(d => d.IdProductAttributeNavigation)
                    .WithMany(p => p.ProductDescriptions)
                    .HasForeignKey(d => d.IdProductAttribute)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductDescription_ibfk_2");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasIndex(e => e.IdProduct, "idProduct");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProduct");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("imageUrl")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductImages_ibfk_1");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("ProductReview");

                entity.HasIndex(e => e.IdClient, "idClient");

                entity.HasIndex(e => e.IdProduct, "idProduct");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ClientScore)
                    .HasColumnType("int(11)")
                    .HasColumnName("clientScore");

                entity.Property(e => e.Commentary)
                    .HasMaxLength(500)
                    .HasColumnName("commentary")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("idClient");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("idProduct");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductReview_ibfk_2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductReview_ibfk_1");
            });

            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.ToTable("StoreInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("bank")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Bic)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("bic");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("fullname")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("tin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
