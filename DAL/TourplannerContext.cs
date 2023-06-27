using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourplannerModel;

namespace DAL
{
    public class TourplannerContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public TourplannerContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("TourDb"));
        }

        public DbSet<TourModel> Tours { get; set; }
        public DbSet<TourLogModel> Tourlogs { get; set; }
    }
}
