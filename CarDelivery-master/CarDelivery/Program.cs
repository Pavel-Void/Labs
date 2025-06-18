using CarDelivery.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDelivery
{
    /// <summary>
    /// Главная точка входа в приложение.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CarDeliveryContext>(options =>
                            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
