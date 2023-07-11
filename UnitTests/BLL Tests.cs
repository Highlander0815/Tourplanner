using DAL;
using BLL;
using TourplannerModel;
using Microsoft.EntityFrameworkCore;
using MiNET.UI;
using MiNET.Utils.Skins;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
using BLL.Exceptions;

namespace UnitTests
{
    public class BLL_Tests
    {
        private DbContextOptions<TourplannerContext> _options;
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;

        [SetUp]
        public void Setup()
        {
            // Setup the Mock Db
            _options = new DbContextOptionsBuilder<TourplannerContext>()
                .UseInMemoryDatabase(databaseName: "TestTourDb")
                .Options;


            // Fill the Mock Db with a tour and corresponding tourlogs (Testing Calculation)
            _tourRepository = new TourRepository(new TourplannerContext(_options));
            _tourLogRepository = new TourLogRepository(new TourplannerContext(_options));
            TourLogModel tourlog1 = new TourLogModel() { DateTime = DateTime.Parse("00:05:20"), Difficulty = DifficultyEnum.Intermediate, TotalTime = TimeSpan.Parse("1"), Rating = 3, Comment="Nothing to mention" };
            TourLogModel tourlog2 = new TourLogModel() { DateTime = DateTime.Parse("00:06:30"), Difficulty = DifficultyEnum.Intermediate, TotalTime = TimeSpan.Parse("2"), Rating = 4, Comment = "Nothing to mention" };
            TourLogModel tourlog3 = new TourLogModel() { DateTime = DateTime.Parse("00:04:50"), Difficulty = DifficultyEnum.Advance, TotalTime = TimeSpan.Parse("2"), Rating = 2, Comment = "Nothing to mention" };
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "Car" };

            _tourRepository.Insert(tour);
            _tourRepository.Save();
            _tourLogRepository.Insert(tourlog1);
            _tourLogRepository.Insert(tourlog2);
            _tourLogRepository.Insert(tourlog3);
            _tourLogRepository.Save();

            var temptour = _tourRepository.GetTourById(1);
            tourlog1.TourModel = temptour;
            tourlog2.TourModel = temptour;
            tourlog3.TourModel = temptour;
            _tourLogRepository.Update(tourlog1);
            _tourLogRepository.Update(tourlog2);
            _tourLogRepository.Update(tourlog3);
            _tourLogRepository.Save();
        }
        [Test]
        public void CalculatePopularity()
        {
            // Arrange
            TourCalculation tourCalculation = new TourCalculation();
            TourModel tour = _tourRepository.GetTourById(1);
            
            // Act
            tourCalculation.CalculatePopularity(tour);

            // Assert
            Assert.That(tour.Popularity, Is.EqualTo(3));
        }
        [Test]
        public void GetTotalTimeAverage()
        {
            // Arrange
            TourCalculation tourCalculation = new TourCalculation();
            TourModel tour = _tourRepository.GetTourById(1);

            // Act
            var result = tourCalculation.GetTotalTimeAverage(tour);

            // Assert
            Assert.That(result, Is.EqualTo(TimeSpan.Parse("1.16:00:00")));
        }
        [Test]
        public void GetAverageRating()
        {
            // Arrange
            TourCalculation tourCalculation = new TourCalculation();
            TourModel tour = _tourRepository.GetTourById(1);

            // Act
            var result = tourCalculation.GetAverageRating(tour);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }
        [Test]
        public void CalculateDifficultyFriendliness()
        {
            // Arrange
            TourCalculation tourCalculation = new TourCalculation();
            TourModel tour = _tourRepository.GetTourById(1);

            // Act
            var result = tourCalculation.CalculateDifficultyFriendliness(tour);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void CalculateChildFriendliness()
        {
            // Arrange
            TourCalculation tourCalculation = new TourCalculation();
            TourModel tour = _tourRepository.GetTourById(1);
            Console.WriteLine(tour.TourLogs.Count());

            // Act
            tourCalculation.CalculateChildFriendliness(tour);

            // Assert
            Assert.That(tour.ChildFriendliness, Is.EqualTo(2));
        }
        [Test]
        //Create a test for Validator TourValidation
        public void TourValidation()
        {
            // Arrange
            Validator validator = new Validator();

            // Act
            var result = validator.TourValidation(_tourRepository.GetTourById(1));          

            // Assert
            Assert.That(result, Is.True);
        }
        [Test]
        //Create a test for Validator TourLogValidation
        public void TourLogValidation()
        {
            // Arrange
            Validator validator = new Validator();

            // Act
            var result = validator.TourLogValidation(_tourLogRepository.GetTourLogsById(1).First());

            // Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void TourValidationFail()
        {
            // Arrange
            Validator validator = new Validator();
            TourModel tour = new TourModel() { Name = "TestTour", Description = "TestDescription", From = "TestFrom", To = "TestTo", TransportType = "NotCarBycicleoOrPedestrian" };

            // Act
            var result = validator.TourValidation(tour);

            // Assert
            Assert.That(result, Is.False);
        }
        [Test]
        public void TourLogValidationFail()
        {
            // Arrange
            Validator validator = new Validator();
            TourLogModel tourlog = new TourLogModel() { DateTime = DateTime.Parse("00:00:00"), Difficulty = DifficultyEnum.Intermediate, TotalTime = TimeSpan.Parse("0"), Rating = 6, Comment = "Nothing to mention" };

            // Act
            var result = validator.TourLogValidation(tourlog);

            // Assert
            Assert.That(result, Is.False);
        }
        [Test]
        public void RestRequestException()
        {
            // Arrange
            TourModel tour = new TourModel("test", "test Description", "falseFrom", "falseTo", "Car");
            RESTHandler restHandler = new RESTHandler();

            //Assert & Act
            Assert.ThrowsAsync<ResponseErrorOfApiException>(async() =>
            {
                var result = await restHandler.Rest.Request(tour);
            });
           
        }
        [Test]
        public async Task Request_ValidTourModel_ReturnsTourWithEstimatedTimeAndDistance()
        {
            // Arrange
            TourModel tour = new TourModel("Tour 1", "Tour description", "Mailberg", "Retz", "Car");
            Rest rest = new Rest();

            // Act
            TourModel result = await rest.Request(tour);

            // Assert
            Assert.IsNotNull(result.EstimatedTime);
            Assert.IsNotNull(result.TourDistance);
            // Weitere Überprüfungen der Eigenschaften des zurückgegebenen TourModels
        }
    }
}