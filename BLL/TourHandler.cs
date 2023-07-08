using BLL.Logging;
using DAL;
using log4net;
using TourplannerModel;

namespace BLL
{
    public class TourHandler
    {
        private ITourRepository tourRepository;
        private static readonly ILoggerWrapper _logger = LoggerFactory.GetLogger();

        public TourHandler(TourplannerContext tourplannerContext)
        {
            tourRepository = new TourRepository(tourplannerContext);
        }
        public void AddTour(TourModel tourmodel)
        {
            tourRepository.Insert(tourmodel);
            tourRepository.Save();
            _logger.Info("Added tour " + tourmodel.Id);
        }
        public void DeleteTour(int tourId)
        {
            tourRepository.Delete(tourId);
            tourRepository.Save();
            _logger.Info("Deleted tour");
        }
        public void UpdateTour(TourModel tourmodel)
        {
            tourRepository.Update(tourmodel);
            tourRepository.Save();
            _logger.Info("Updated tour");
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
