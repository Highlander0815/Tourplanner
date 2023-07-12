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
    }
}
