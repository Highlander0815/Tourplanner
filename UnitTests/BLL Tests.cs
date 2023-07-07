using DAL;
using UI;
using BLL;
using TourplannerModel;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void AddTourLogTest()
        {
            // Arrange
            InMemoryTourLogHandler tourLogHandler = new InMemoryTourLogHandler();
            TourLogModel tourLogModel = new TourLogModel();
            // Act
            tourLogHandler.AddTourLog(tourLogModel);
            // Assert
            Assert.That(tourLogHandler.GetTourLogs().Count(), Is.EqualTo(1));
        }
    }
}