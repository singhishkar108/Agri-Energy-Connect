using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergy.Models;

public partial class AgriEnergyContext : DbContext
{
    public AgriEnergyContext()
    {
    }

    public AgriEnergyContext(DbContextOptions<AgriEnergyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localDB)\\MSSQLLocalDb;Database=AgriEnergy;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B79EC0F2BE");

            entity.ToTable("Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Cart_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__CATEGORI__E7DA297C3BFC9059");

            entity.ToTable("CATEGORIES");

            entity.HasIndex(e => e.CategoryName, "UQ__CATEGORI__9374460F7500B836").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__PRODUCTS__52B417638C6ACD0A");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Availability)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("AVAILABILITY");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.FarmerId).HasColumnName("FARMER_ID");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.ProductDescription)
                .HasColumnType("text")
                .HasColumnName("PRODUCT_DESCRIPTION");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.ProductionDate).HasColumnName("PRODUCTION_DATE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__PRODUCTS__CATEGO__2C3393D0");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Products)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FK__PRODUCTS__FARMER__2B3F6F97");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS__F3BEEBFFE425D372");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.FirebaseUid, "UQ__USERS__97B07D8FE957C5E6").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("BIO");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirebaseUid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIREBASE_UID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
