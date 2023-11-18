using WeatherConditions.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherConditions.DAL
{
    public class WeatherContext : DbContext
    {

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        public DbSet<Weather> Weathers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server= .\\SQLEXPRESS;Database=weatherdb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
    }
}
