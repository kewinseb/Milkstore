using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MilkStore.Models;

namespace MilkStore.Data;

public partial class MilkstoreDbContext : DbContext
{
    public MilkstoreDbContext()
    {
    }

    public MilkstoreDbContext(DbContextOptions<MilkstoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardDetail> CardDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HHOTP7Q;Database=Milkstore;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardDetail>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__CardDeta__4D5BC491844A3D3D");

            entity.Property(e => e.CardId).HasColumnName("cardId");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("cardNumber");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("cvv");
            entity.Property(e => e.UserEmailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_emailId");

            entity.HasOne(d => d.UserEmail).WithMany(p => p.CardDetails)
                .HasForeignKey(d => d.UserEmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CardDetai__User___534D60F1");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03B88746E28E");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.ProductProductId).HasColumnName("Product_productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserEmailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_emailId");

            entity.HasOne(d => d.ProductProduct).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Product_pr__4316F928");

            entity.HasOne(d => d.UserEmail).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserEmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__User_email__4222D4EF");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809335DA27F7321");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserEmailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_emailId");

            entity.HasOne(d => d.UserEmail).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserEmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__User_ema__47DBAE45");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK__OrderIte__5EEE527321D87DB5");

            entity.Property(e => e.OrderDetailsId).HasColumnName("orderDetailsId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.OrdersOrderId).HasColumnName("Orders_orderId");
            entity.Property(e => e.PriceAtOrder)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("priceAtOrder");
            entity.Property(e => e.ProductProductId).HasColumnName("Product_productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TrackingNumber).HasColumnName("trackingNumber");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.OrdersOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrdersOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__4CA06362");

            entity.HasOne(d => d.ProductProduct).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__4D94879B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D16ACCDF16A4");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.EstimatedDelivery).HasColumnName("estimatedDelivery");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("productName");
            entity.Property(e => e.StockQuantity).HasColumnName("stockQuantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<ShipmentDetail>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PK__Shipment__472178019705953B");

            entity.Property(e => e.ShipmentId).HasColumnName("shipmentId");
            entity.Property(e => e.DeliveryDate).HasColumnName("deliveryDate");
            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("deliveryStatus");
            entity.Property(e => e.OrdersOrderId).HasColumnName("Orders_orderId");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("shippingAddress");

            entity.HasOne(d => d.OrdersOrder).WithMany(p => p.ShipmentDetails)
                .HasForeignKey(d => d.OrdersOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShipmentD__Order__5629CD9C");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__9B57CF7229E9D5AC");

            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paymentMode");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paymentStatus");
            entity.Property(e => e.TotalAmountPaid)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmountPaid");
            entity.Property(e => e.TransactionDate).HasColumnName("transactionDate");
            entity.Property(e => e.UserEmailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_emailId");

            entity.HasOne(d => d.UserEmail).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserEmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__User___5070F446");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("PK__Users__87355E72966BE19D");

            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("emailId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.PinCode).HasColumnName("pinCode");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("user")
                .HasColumnName("role");
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
