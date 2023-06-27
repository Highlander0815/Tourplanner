using DAL;
using Microsoft.Extensions.Configuration;
using TourplannerModel;

namespace BLL
{
    public class TourHandler
    {
        private ITourRepository tourRepository;
        public TourHandler(IConfiguration configuration)
        {
            tourRepository = new TourRepository(new TourplannerContext(configuration));
        }
        public void AddTour(TourModel tourmodel)
        {
            tourRepository.Insert(tourmodel);
            tourRepository.Save();
        }
        public void DeleteTour(int tourId)
        {
            tourRepository.Delete(tourId);
            tourRepository.Save();
        }
        public void UpdateTour(TourModel tourmodel)
        {
            tourRepository.Update(tourmodel);
            tourRepository.Save();
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
