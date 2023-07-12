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
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TourplannerContext>()
                .UseInMemoryDatabase(databaseName: "TestDbDAL")
                .Options;
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            Assert.That(_tourRepository.GetTours().Count(), Is.EqualTo(1));
        }

        [Test]
        public void UpdateTour()
        {
            // Arrange
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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

            _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel temptour = _tourRepository.GetTours().First();
            temptour.Name = "BisaTour";

            // Act
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            _tourRepository.Update(temptour);
            _tourRepository.Save();

            // Assert
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            Assert.That(_tourRepository.GetTourById(temptour.Id).Name, Is.EqualTo("BisaTour"));
        }

        [Test]
        public void DeleteTour()
        {
            // Arrange
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            _tourRepository.Delete(1);
            _tourRepository.Save();

            // Assert
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            Assert.That(_tourRepository.GetTours().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTourById()
        {
            // Arrange
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            var id = _tourRepository.GetTours().First().Id;
            var result = _tourRepository.GetTourById(id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(id));

        }
        [Test]
        public void GetTours()
        {
            // Arrange
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "TestTransportType" };
            TourModel tour2 = new TourModel() { Name = "TestTour2", Description = "TestDescription2", From = "TestFrom2", To = "TestTo2", TransportType = "TestTransportType2" };
            _tourRepository.Insert(tour);
            _tourRepository.Insert(tour2);
            _tourRepository.Save();

            // Act
            _tourRepository = new TourRepository(new TourplannerContext(_options));
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
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };

            // Act
            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            // Assert
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(1));
        }

        [Test]
        public void UpdateTourLog()
        {
            // Arrange
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            var temptourlog = _tourLogRepository.GetTourLogs().First();
            temptourlog.Rating = 5;

            // Act
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            _tourLogRepository.Update(temptourlog);
            _tourLogRepository.Save();

            // Assert
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            Assert.That(_tourLogRepository.GetTourLogs().First().Rating, Is.EqualTo(5));
        }

        [Test]
        public void DeleteTourLog()
        {
            // Arrange
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            var temptourlog = _tourLogRepository.GetTourLogs().First();

            // Act
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            _tourLogRepository.Delete(temptourlog.Id);
            _tourLogRepository.Save();

            // Assert
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTourLogById()
        {
            // Arrange
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "TestTransportType" };

            _tourRepository.Insert(tour);
            _tourRepository.Save();
            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Save();

            _tourRepository = new TourRepository(new TourplannerContext(_options));
            var temptour = _tourRepository.GetTourById(1);
            tourlog.TourModelId = temptour.Id;

            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            _tourLogRepository.Update(tourlog);
            _tourLogRepository.Save();

            // Act
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            _tourLogRepository.GetTourLogsById(1);

            // Assert
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            Assert.That(_tourLogRepository.GetTourLogs().Count(), Is.EqualTo(1));
        }
        [Test]
        public void GetTourLogs()
        {
            // Arrange
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };
            TourLogModel tourlog1 = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("0"), Rating = 2, Comment = "Fake" };

            _tourLogRepository.Insert(tourlog);
            _tourLogRepository.Insert(tourlog1);
            _tourLogRepository.Save();

            // Act
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            var result = _tourLogRepository.GetTourLogs();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
