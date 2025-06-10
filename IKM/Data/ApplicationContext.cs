using IKM.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IKM.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Driver> Driver => Set<Driver>();
        public DbSet<Car> Car => Set<Car>();
        public DbSet<DriverCar> DriverCar => Set<DriverCar>();
        public DbSet<DriverProperty> DriverProperty => Set<DriverProperty>();


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverCar>()
                .HasKey(pa => new { pa.Driver_ID, pa.Car_ID });

            // Добавляем конфигурации для связей, если они не заданы через атрибуты или Fluent API
            modelBuilder.Entity<DriverCar>()
               .HasOne(c => c.Driver)
               .WithMany() // Если у Persone нет навигационного свойства
               .HasForeignKey(c => c.Driver_ID);

            modelBuilder.Entity<DriverCar>()
               .HasOne(c => c.Car)
               .WithMany() // Если у Burger нет навигационного свойства
               .HasForeignKey(c => c.Car_ID);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin");
        }
    }
}
