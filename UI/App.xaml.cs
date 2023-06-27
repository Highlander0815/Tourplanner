using System;
using System.Data.Entity;
using System.Windows;
using DAL;
using Microsoft.Extensions.Configuration;
using TourplannerModel;

namespace Tourplanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            using (TourplannerContext tourplannerContext = new(configuration))
            {
                try
                {
                    tourplannerContext.Database.EnsureDeleted();
                    tourplannerContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Make sure you are running a local PostgresDB with the postgis extension installed and username/password and port are correct!");
                    Console.WriteLine("E.g. by using the docker compose file in the root of this repository.");
                    Console.WriteLine("docker compose up -d");
                }

                ITourRepository tourRepository = new TourRepository(tourplannerContext);
                ITourLogRepository tourLogRepository = new TourlogRepository(tourplannerContext);

                tourRepository.Insert(new TourModel()
                {
                    Name = "Testtour_1",
                    Description = "Just a test",
                    From = "Salzburg",
                    To = "Wien",
                    TransportType = "Car",
                    TourDistance = "56",
                    EstimatedTime = "78",
                    Image = "tour_1"
                });
                tourRepository.Save();
            }
        }
    }
}
