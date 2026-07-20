using Microsoft.EntityFrameworkCore;
using PetSpaAPI.Models;

namespace PetSpaAPI.Data
{
    public class PetSpaDbContext : DbContext
    {
        public PetSpaDbContext(DbContextOptions<PetSpaDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePrice> ServicePrices { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TimeSlotConfig> TimeSlotConfigs { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth").HasColumnType("date");
                entity.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(10).HasDefaultValue("other");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(20).IsRequired().HasDefaultValue("customer");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).IsRequired().HasDefaultValue("active");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasColumnName("type").HasMaxLength(20).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Unit).HasColumnName("unit").HasMaxLength(50);
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity").HasDefaultValue(0);
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
                entity.Property(e => e.Brand).HasColumnName("brand").HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Service configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services");
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes").IsRequired();
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
                entity.Property(e => e.PricingMethod).HasColumnName("pricing_method").HasMaxLength(20).HasDefaultValue("fixed");
                
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Services)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ServicePrice configuration
            modelBuilder.Entity<ServicePrice>(entity =>
            {
                entity.ToTable("service_prices");
                entity.HasKey(e => e.PriceId);
                entity.Property(e => e.PriceId).HasColumnName("price_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.PetType).HasColumnName("pet_type").HasMaxLength(10).IsRequired();
                entity.Property(e => e.MinWeight).HasColumnName("min_weight").HasColumnType("decimal(5,2)").HasDefaultValue(0);
                entity.Property(e => e.MaxWeight).HasColumnName("max_weight").HasColumnType("decimal(5,2)").HasDefaultValue(999);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(e => e.Service)
                    .WithMany(s => s.ServicePrices)
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //  Supplier configuration
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");
                entity.HasKey(e => e.SupplierId);
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(500);
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
                entity.Property(e => e.BankAccount).HasColumnName("bank_account").HasMaxLength(100);
            });

            // PurchaseOrder configuration
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("purchase_orders");
                entity.HasKey(e => e.PurchaseOrderId);
                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchase_order_id");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.StaffId).HasColumnName("staff_id");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Supplier)
                    .WithMany(s => s.PurchaseOrders)
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Staff)
                    .WithMany()
                    .HasForeignKey(e => e.StaffId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PurchaseOrderItem configuration
            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("purchase_order_items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchase_order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.PurchaseOrder)
                    .WithMany(p => p.Items)
                    .HasForeignKey(e => e.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(500);

                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<Customer>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CustomerAddress configuration
            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("customer_addresses");
                entity.HasKey(e => e.AddressId);
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20).IsRequired();
                entity.Property(e => e.AddressLine).HasColumnName("address_line").HasMaxLength(500).IsRequired();
                
                entity.Property(e => e.ProvinceId).HasColumnName("province_id").HasMaxLength(10).IsRequired();
                entity.Property(e => e.ProvinceName).HasColumnName("province_name").HasMaxLength(100).IsRequired();
                
                entity.Property(e => e.WardId).HasColumnName("ward_id").HasMaxLength(10).IsRequired();
                entity.Property(e => e.WardName).HasColumnName("ward_name").HasMaxLength(100).IsRequired();
                
                entity.Property(e => e.IsDefault).HasColumnName("is_default").HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Pet configuration
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pets");
                entity.HasKey(e => e.PetId);
                entity.Property(e => e.PetId).HasColumnName("pet_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Breed).HasColumnName("breed").HasMaxLength(100);
                entity.Property(e => e.Age).HasColumnName("age");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(500);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Pets)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Promotion configuration
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("promotions");
                entity.HasKey(e => e.PromotionId);
                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
                entity.Property(e => e.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
                entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent").HasColumnType("decimal(5,2)");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointments");
                entity.HasKey(e => e.AppointmentId);
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.PetInfo).HasColumnName("pet_info").HasColumnType("TEXT");
                entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
                entity.Property(e => e.TimeSlot).HasColumnName("time_slot").HasMaxLength(20).HasDefaultValue("09:00");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("pending");          
                entity.Property(e => e.GuestName).HasColumnName("guest_name").HasMaxLength(100);
                entity.Property(e => e.GuestPhone).HasColumnName("guest_phone").HasMaxLength(20);
                entity.Property(e => e.GuestAddress).HasColumnName("guest_address").HasMaxLength(500);
                entity.Property(e => e.PetType).HasColumnName("pet_type").HasMaxLength(20).HasDefaultValue("dog");
                entity.Property(e => e.PetBreed).HasColumnName("pet_breed").HasMaxLength(100);
                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(e => e.Service)
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TimeSlotConfig configuration
            modelBuilder.Entity<TimeSlotConfig>(entity =>
            {
                entity.ToTable("time_slot_config");
                entity.HasKey(e => e.SlotId);
                entity.Property(e => e.SlotId).HasColumnName("slot_id");
                entity.Property(e => e.TimeSlot).HasColumnName("time_slot").HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaxBookings).HasColumnName("max_bookings").HasDefaultValue(3);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
                entity.HasIndex(e => e.TimeSlot).IsUnique();
            });

            // CartItem configuration
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("cart_items");
                entity.HasKey(e => e.CartItemId);
                entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity").HasDefaultValue(1);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Service)
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                
                // Shipping Info
                entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
                entity.Property(e => e.ShippingFullName).HasColumnName("shipping_full_name").HasMaxLength(100);
                entity.Property(e => e.ShippingPhone).HasColumnName("shipping_phone").HasMaxLength(20);
                entity.Property(e => e.ShippingAddressLine).HasColumnName("shipping_address_line").HasMaxLength(500);
                entity.Property(e => e.ShippingProvinceId).HasColumnName("shipping_province_id").HasMaxLength(10);
                entity.Property(e => e.ShippingProvinceName).HasColumnName("shipping_province_name").HasMaxLength(100);
                entity.Property(e => e.ShippingWardId).HasColumnName("shipping_ward_id").HasMaxLength(10);
                entity.Property(e => e.ShippingWardName).HasColumnName("shipping_ward_name").HasMaxLength(100);
                
                // Pricing
                entity.Property(e => e.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.ShippingFee).HasColumnName("shipping_fee").HasColumnType("decimal(18,2)").HasDefaultValue(25000);
                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
                entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
                
                entity.Property(e => e.Note).HasColumnName("note").HasColumnType("TEXT");
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method").HasMaxLength(20).HasDefaultValue("cod");
                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status").HasMaxLength(20).HasDefaultValue("pending");
                entity.Property(e => e.OrderStatus).HasColumnName("order_status").HasMaxLength(20).HasDefaultValue("pending");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TransId).HasColumnName("trans_id").HasMaxLength(50);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.HasKey(e => e.OrderItemId);
                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ServiceId).HasColumnName("service_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.Items)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Service)
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // AuditLog configuration
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("audit_logs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Action).HasColumnName("action").HasMaxLength(500).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
