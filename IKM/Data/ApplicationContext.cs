using IKM.Models;
using Microsoft.EntityFrameworkCore;

namespace IKM.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Driver> Driver => Set<Driver>();
        public DbSet<Car> Car => Set<Car>();
        public DbSet<Racing> Racing => Set<Racing>();


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin");
        }

    }

}