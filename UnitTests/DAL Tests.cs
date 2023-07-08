using Moq;
using TourplannerModel;
using DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using BLL;
using System.Diagnostics;

namespace UnitTests
{
    public class DAL_Tests
    {
        private DbContextOptions<TourplannerContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TourplannerContext>()
                .UseInMemoryDatabase(databaseName: "TestTourDb")
                .Options;
        }
        [TearDown]
        public void TearDown()
        {
            using (var context = new TourplannerContext(_options))
            {
                context.Tours.RemoveRange(context.Tours);
                context.Tourlogs.RemoveRange(context.Tourlogs);
                context.SaveChanges();
            }
        }


        [Test]
        public void InsertTour()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel()
            {
                Name = "TestTour",
                Description = "TestDescription",
                From = "TestFrom",
                To = "TestTo",
                TransportType = "TestTransportType"
            };

            // Act
            _tourRepository.Insert(tour);
            _tourRepository.Save();

            // Assert
            Assert.That(_tourRepository.GetTourById(1).Name, Is.EqualTo("TestTour"));
        }

        [Test]
        public void UpdateTour()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel()
            {
                Name = "TestTour",
                Description = "TestDescription",
                From = "TestFrom",
                To = "TestTo",
                TransportType = "TestTransportType"
            };
            _tourRepository.Insert(tour);
            _tourRepository.Save();
            TourModel temptour = _tourRepository.GetTourById(1);
            temptour.Name = "BisaTour";

            // Act
            _tourRepository.Update(temptour);
            _tourRepository.Save();

            // Assert
            Assert.That(_tourRepository.GetTourById(1).Name, Is.EqualTo("BisaTour"));
        }

        [Test]
        public void DeleteTour()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel()
            {
                Name = "TestTour",
                Description = "TestDescription",
                From = "TestFrom",
                To = "TestTo",
                TransportType = "TestTransportType"
            };
            _tourRepository.Insert(tour);
            _tourRepository.Save();

            // Act
            _tourRepository.Delete(1);
            _tourRepository.Save();

            // Assert
            Assert.That(_tourRepository.GetTours().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTourById()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel()
            {
                Name = "TestTour",
                Description = "TestDescription",
                From = "TestFrom",
                To = "TestTo",
                TransportType = "TestTransportType"
            };
            _tourRepository.Insert(tour);
            _tourRepository.Save();

            // Act
            var result = _tourRepository.GetTourById(1);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));

        }
        [Test]
        public void GetTours()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "TestTransportType" };
            TourModel tour2 = new TourModel() { Name = "TestTour2", Description = "TestDescription2", From = "TestFrom2", To = "TestTo2", TransportType = "TestTransportType2" };
            _tourRepository.Insert(tour);
            _tourRepository.Insert(tour2);
            _tourRepository.Save();

            // Act
            var result = _tourRepository.GetTours();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Name, Is.EqualTo("TestTour"));
            Assert.That(result.ElementAt(1).Name, Is.EqualTo("TestTour2"));
        }
        [Test]
        public void InsertTourLog()
        {
            // Arrange
            TourLogRepository _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };

            // Act
            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            // Assert
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(1));
        }

        [Test]
        public void UpdateTourLog()
        {
            // Arrange
            TourLogRepository _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();
            var temptourlog = _tourLogRepository.GetTourLogs().First();
            temptourlog.Rating = 5;

            // Act
            _tourLogRepository.Update(temptourlog);
            _tourLogRepository.Save();

            // Assert
            Assert.That(_tourLogRepository.GetTourLogs().First().Rating, Is.EqualTo(5));
        }

        [Test]
        public void DeleteTourLog()
        {
            // Arrange
            TourLogRepository _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();
            var temptourlog = _tourLogRepository.GetTourLogs().First();

            // Act
            _tourLogRepository.Delete(temptourlog.Id);
            _tourLogRepository.Save();

            // Assert
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTourLogById()
        {
            // Arrange
            TourRepository _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourLogRepository _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "TestTransportType" };

            _tourRepository.Insert(tour);
            _tourRepository.Save();
            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            var temptour = _tourRepository.GetTourById(1);
            tourlog.TourModel = temptour;
            _tourLogRepository.Update(tourlog);
            _tourLogRepository.Save();

            // Act
            _tourLogRepository.GetTourLogsById(1);

            // Assert
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(1));
        }
        [Test]
        public void GetTourLogs()
        {
            // Arrange
            TourLogRepository _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };
            TourLogModel tourlog1 = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2 };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Insert(tourlog1);
            _tourLogRepository.Save();

            // Act
            var result = _tourLogRepository.GetTourLogs();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
