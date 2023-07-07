using BLL.Logging;
using DAL;
using log4net;
using TourplannerModel;

namespace BLL
{
    public class InMemoryTourHandler
    {
        private InMemoryTourRepository _tourRepository;
        public InMemoryTourHandler()
        {
            _tourRepository = new InMemoryTourRepository();
        }
        public void AddTour(TourModel tourmodel)
        {
            _tourRepository.Insert(tourmodel);
        }
        public void DeleteTour(TourModel tour)
        {
            _tourRepository.Delete(tour);
        }
        public void UpdateTour(TourModel tourmodel)
        {
            // No test for this method
        }
        public IEnumerable<TourModel> GetTours()
        {
            return _tourRepository.GetTours();
        }
        public TourModel GetTour(int id)
        {
            return _tourRepository.GetTourById(id);
        }
    }
}
