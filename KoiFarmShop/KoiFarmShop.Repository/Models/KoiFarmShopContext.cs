﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KoiFarmShop.Repository.Models;

public partial class KoiFarmShopContext : DbContext
{
    public KoiFarmShopContext(DbContextOptions<KoiFarmShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiOrder> KoiOrders { get; set; }

    public virtual DbSet<KoiOrderDetail> KoiOrderDetails { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B0FE51E0A");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Img).HasMaxLength(250);
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B89520E915");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Customer__UserID__398D8EEE");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.KoiFishRatingId).HasName("PK__Feedback__225C9B3193C3ACD6");

            entity.ToTable("Feedback");

            entity.Property(e => e.KoiFishRatingId)
                .ValueGeneratedNever()
                .HasColumnName("KoiFish_RatingID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.KoiFishId).HasColumnName("KoiFishID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.KoiFish).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.KoiFishId)
                .HasConstraintName("FK__Feedback__KoiFis__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__4BAC3F29");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.KoiFishId).HasName("PK__KoiFish__44D353C5B7453102");

            entity.ToTable("KoiFish");

            entity.Property(e => e.KoiFishId)
                .ValueGeneratedNever()
                .HasColumnName("KoiFishID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Origin).HasMaxLength(50);
            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFish__Categor__403A8C7D");

            entity.HasOne(d => d.Size).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__KoiFish__SizeID__412EB0B6");
        });

        modelBuilder.Entity<KoiOrder>(entity =>
        {
            entity.HasKey(e => e.KoiOrderId).HasName("PK__KoiOrder__1EA629AFB10BE3CA");

            entity.ToTable("KoiOrder");

            entity.Property(e => e.KoiOrderId)
                .ValueGeneratedNever()
                .HasColumnName("KoiOrderID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.KoiOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__KoiOrder__Custom__440B1D61");
        });

        modelBuilder.Entity<KoiOrderDetail>(entity =>
        {
            entity.HasKey(e => e.KoiOrderDetailId).HasName("PK__KoiOrder__7E16BE2D9FDA74C2");

            entity.ToTable("KoiOrderDetail");

            entity.Property(e => e.KoiOrderDetailId)
                .ValueGeneratedNever()
                .HasColumnName("KoiOrderDetailID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.KoiFishId).HasColumnName("KoiFishID");
            entity.Property(e => e.KoiOrderId).HasColumnName("KoiOrderID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.KoiFish).WithMany(p => p.KoiOrderDetails)
                .HasForeignKey(d => d.KoiFishId)
                .HasConstraintName("FK__KoiOrderD__KoiFi__47DBAE45");

            entity.HasOne(d => d.KoiOrder).WithMany(p => p.KoiOrderDetails)
                .HasForeignKey(d => d.KoiOrderId)
                .HasConstraintName("FK__KoiOrderD__KoiOr__46E78A0C");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__83BD095AC1C67F72");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId)
                .ValueGeneratedNever()
                .HasColumnName("SizeID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC1C5098B5");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}