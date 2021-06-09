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
                optionsBuilder.UseSqlServer("Server=tcp:dpspapidbserver.database.windows.net,1433;Initial Catalog=DPSP Api_db;Persist Security Info=False;User ID=alexs;Password=AMS25051980s34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AddressDelivery>(entity =>
            {
                entity.ToTable("AddressDelivery");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.ApartmentNum).HasColumnName("apartmentNum");

                entity.Property(e => e.FloorNum).HasColumnName("floorNum");

                entity.Property(e => e.FrontDoor).HasColumnName("frontDoor");

                entity.Property(e => e.Intercom)
                    .HasMaxLength(20)
                    .HasColumnName("intercom");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdContacts).HasColumnName("idContacts");

                entity.Property(e => e.IdPersonalInfo).HasColumnName("idPersonalInfo");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdContactsNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdContacts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Client__idContac__619B8048");

                entity.HasOne(d => d.IdPersonalInfoNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdPersonalInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Client__idPerson__60A75C0F");
            });

            modelBuilder.Entity<ClientAddress>(entity =>
            {
                entity.ToTable("ClientAddress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAddress).HasColumnName("idAddress");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientAdd__idAdd__10566F31");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ClientAddresses)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientAdd__idCli__0F624AF8");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.ToTable("Courier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdContacts).HasColumnName("idContacts");

                entity.Property(e => e.IdPersonalInfo).HasColumnName("idPersonalInfo");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.OrderQuantity)
                    .HasColumnName("orderQuantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdContactsNavigation)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.IdContacts)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courier__idConta__66603565");

                entity.HasOne(d => d.IdPersonalInfoNavigation)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.IdPersonalInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courier__idPerso__656C112C");
            });

            modelBuilder.Entity<OrderFinished>(entity =>
            {
                entity.ToTable("OrderFinished");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientScore).HasColumnName("clientScore");

                entity.Property(e => e.Commentary).HasColumnName("commentary");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderFinisheds)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderFini__idOrd__74AE54BC");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ordered>(entity =>
            {
                entity.ToTable("Ordered");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Commentary).HasColumnName("commentary");

                entity.Property(e => e.CodeToFinish)
                    .HasColumnName("codeToFinish")
                    .HasDefaultValueSql("(('0000'))");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("date")
                    .HasColumnName("deliveryDate");

                entity.Property(e => e.DeliveryTimeFrom).HasColumnName("deliveryTimeFrom");

                entity.Property(e => e.DeliveryTimeTo).HasColumnName("deliveryTimeTo");

                entity.Property(e => e.IdAddress).HasColumnName("idAddress");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.IdCourier).HasColumnName("idCourier");

                entity.Property(e => e.IdOrderStatus).HasColumnName("idOrderStatus");

                entity.Property(e => e.OrderDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDateTime");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.Summ)
                    .HasColumnName("summ")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ordered__idAddre__6EF57B66");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ordered__idClien__6FE99F9F");

                entity.HasOne(d => d.IdCourierNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdCourier)
                    .HasConstraintName("FK__Ordered__idCouri__70DDC3D8");

                entity.HasOne(d => d.IdOrderStatusNavigation)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.IdOrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ordered__idOrder__71D1E811");
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.ToTable("PersonalInfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avail)
                    .HasColumnName("avail")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdStoreInfo).HasColumnName("idStoreInfo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((0.0))");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__idCateg__00200768");

                entity.HasOne(d => d.IdStoreInfoNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdStoreInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__idStore__01142BA1");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("ProductAttribute");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdParentCategory).HasColumnName("idParentCategory");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(300)
                    .HasColumnName("imageUrl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdParentCategoryNavigation)
                    .WithMany(p => p.InverseIdParentCategoryNavigation)
                    .HasForeignKey(d => d.IdParentCategory)
                    .HasConstraintName("FK__ProductCa__idPar__7B5B524B");
            });

            modelBuilder.Entity<ProductCompos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Summ).HasColumnName("summ");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ProductCompos)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCo__idOrd__08B54D69");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductCompos)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCo__idPro__07C12930");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AttrValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("attrValue");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdProductAttribute).HasColumnName("idProductAttribute");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductDescriptions)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductDe__idPro__03F0984C");

                entity.HasOne(d => d.IdProductAttributeNavigation)
                    .WithMany(p => p.ProductDescriptions)
                    .HasForeignKey(d => d.IdProductAttribute)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductDe__idPro__04E4BC85");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("imageUrl");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIm__idPro__2A164134");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("ProductReview");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientScore).HasColumnName("clientScore");

                entity.Property(e => e.Commentary).HasColumnName("commentary");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductRe__idCli__0C85DE4D");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductRe__idPro__0B91BA14");
            });

            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.ToTable("StoreInfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("bank");

                entity.Property(e => e.Bic)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("bic");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
