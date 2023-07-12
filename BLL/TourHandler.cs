using BLL.Logging;
using DAL;
using log4net;
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
        public TourHandler() 
        {
            tourRepository = new TourRepository();
        }
        public void AddTour(TourModel tourmodel)
        {
            tourRepository.Insert(tourmodel);
            tourRepository.Save();
            tourRepository.Dispose();
        }
        public void DeleteTour(int tourId)
        {
            tourRepository.Delete(tourId);
            tourRepository.Save();
            tourRepository.Dispose();
        }
        public void UpdateTour(TourModel tourmodel)
        {
            tourRepository.Update(tourmodel);
            tourRepository.Save();
            tourRepository.Dispose();
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
