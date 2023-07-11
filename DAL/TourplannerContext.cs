using Microsoft.EntityFrameworkCore;
using TourplannerModel;
using System.Configuration;

namespace DAL
{
    public class TourplannerContext : DbContext
    {
        public TourplannerContext(DbContextOptions<TourplannerContext> options) : base(options) { }

        public DbSet<TourModel> Tours { get; set; }
        public DbSet<TourLogModel> Tourlogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["tour_db"].ConnectionString);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
