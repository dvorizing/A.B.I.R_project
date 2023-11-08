using System;
using System.Collections.Generic;
using AbirCarsProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AbirCarsProject.Data;

public partial class CarsDbContext : DbContext
{
    public CarsDbContext()
    {
    }

    public CarsDbContext(DbContextOptions<CarsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<GaragePermission> GaragePermissions { get; set; }

    public virtual DbSet<GarageVisit> GarageVisits { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }
      
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ToDoDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B81F4ABA85");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105348FBF5A3C").IsUnique();

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("PK__Garage__5D8BEEB1200E23BD");

            entity.ToTable("Garage");

            entity.Property(e => e.GarageId)
                .ValueGeneratedNever()
                .HasColumnName("GarageID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GarageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GaragePermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__GaragePe__EFA6FB0F03077E1E");

            entity.Property(e => e.PermissionId)
                .ValueGeneratedNever()
                .HasColumnName("PermissionID");
            entity.Property(e => e.GarageId).HasColumnName("GarageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Garage).WithMany(p => p.GaragePermissions)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("FK__GaragePer__Garag__37A5467C");

            entity.HasOne(d => d.User).WithMany(p => p.GaragePermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__GaragePer__UserI__36B12243");
        });

        modelBuilder.Entity<GarageVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__GarageVi__4D3AA1BE2B8A4CA3");

            entity.Property(e => e.VisitId)
                .ValueGeneratedNever()
                .HasColumnName("VisitID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.GarageId).HasColumnName("GarageID");
            entity.Property(e => e.ServiceDescription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VisitDate).HasColumnType("date");

            entity.HasOne(d => d.Customer).WithMany(p => p.GarageVisits)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__GarageVis__Custo__32E0915F");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageVisits)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("FK__GarageVis__Garag__33D4B598");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB0FAFD0F513");

            entity.Property(e => e.PermissionId)
                .ValueGeneratedNever()
                .HasColumnName("PermissionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Permissio__Custo__2A4B4B5E");

            entity.HasOne(d => d.User).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Permissio__UserI__29572725");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC5DAE0CD9");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B28160377F");

            entity.HasIndex(e => e.Vin, "UQ__Vehicles__C5DF234C1B7D13DB").IsUnique();

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("VehicleID");
            entity.Property(e => e.LastServiceDate).HasColumnType("date");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("VIN");

            entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Vehicles__OwnerI__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
