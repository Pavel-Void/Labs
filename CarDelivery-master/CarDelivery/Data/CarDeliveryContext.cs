using CarDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDelivery.Data;

/// <summary>
/// Контекст базы данных CarDelivery.
/// </summary>
public partial class CarDeliveryContext : DbContext
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public CarDeliveryContext()
    {
    }

    /// <summary>
    /// Конструктор с параметрами.
    /// </summary>
    public CarDeliveryContext(DbContextOptions<CarDeliveryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cars> Cars { get; set; }

    public virtual DbSet<Complectations> Complectations { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    /// <summary>
    /// Конфигурирует параметры подключения к базе данных.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CarDelivery;Username=postgres;Password=3547");

    /// <summary>
    /// Конфигурирует модель базы данных.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cars>(entity =>
        {
            entity.HasKey(e => e.Carid).HasName("cars_pkey");

            entity.ToTable("cars");

            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.Complectationid).HasColumnName("complectationid");
            entity.Property(e => e.Make)
                .HasMaxLength(100)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");

            entity.HasOne(d => d.Complectations).WithMany(p => p.Cars)
                .HasForeignKey(d => d.Complectationid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cars_complectationid_fkey");
        });

        modelBuilder.Entity<Complectations>(entity =>
        {
            entity.HasKey(e => e.Complectationid).HasName("complectations_pkey");

            entity.ToTable("complectations");

            entity.Property(e => e.Complectationid).HasColumnName("complectationid");
            entity.Property(e => e.Engine)
                .HasMaxLength(100)
                .HasColumnName("engine");
            entity.Property(e => e.Equipment)
                .HasMaxLength(100)
                .HasColumnName("equipment");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Cars).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Carid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_carid_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_userid_fkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
