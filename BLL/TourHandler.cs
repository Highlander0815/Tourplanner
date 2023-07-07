using BLL.Logging;
using DAL;
using TourplannerModel;

namespace BLL
{
    public class TourHandler
    {
        private ITourRepository tourRepository;
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();
        public TourHandler(TourplannerContext tourplannerContext)
        {
            tourRepository = new TourRepository(tourplannerContext);            
        }
        public void AddTour(TourModel tourmodel)
        {
            tourRepository.Insert(tourmodel);
            tourRepository.Save();
            logger.Info("Added tour");
        }
        public void DeleteTour(int tourId)
        {
            tourRepository.Delete(tourId);
            tourRepository.Save();
            logger.Info("Deleted tour");
        }
        public void UpdateTour(TourModel tourmodel)
        {
            tourRepository.Update(tourmodel);
            tourRepository.Save();
            logger.Info("Updated tour");
        }
        public IEnumerable<TourModel> GetTours()
        {
            return tourRepository.GetTours();
        }
        public TourModel GetTour(int id)
        {
            return tourRepository.GetTourById(id);
        }
    }
}
