using DAL;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using TourplannerModel;

namespace BLL
{
    public class TourHandler
    {
        private ITourRepository tourRepository;
        public TourHandler(TourplannerContext tourplannerContext)
        {
            tourRepository = new TourRepository(tourplannerContext);            
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
