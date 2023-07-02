using DAL;

namespace BLL
{
    public class DbManager
    {
        private TourplannerContext _tourplannerContext;
        public DbManager(TourplannerContext tourplannerContext)
        {
            _tourplannerContext = tourplannerContext;
        }
        public void EnsureDbCreated()
        {
            try
            {
                _tourplannerContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Make sure you are running a local PostgresDB with the postgis extension installed and username/password and port are correct!");
                Console.WriteLine("E.g. by using the docker compose file in the root of this repository.");
                Console.WriteLine("docker compose up -d");
            }
        }
    }
}
