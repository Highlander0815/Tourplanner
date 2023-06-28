using DAL;
using Microsoft.Extensions.Configuration;
using TourplannerModel;

namespace BLL
{
    public class TourLogHandler
    {
        private ITourLogRepository tourLogRepository;
        public TourLogHandler(IConfiguration configuration)
        {
            tourLogRepository = new TourLogRepository(new TourplannerContext(configuration));
        }
        public void AddTourLog(TourLogModel tourlogmodel)
        {
            tourLogRepository.Insert(tourlogmodel);
            tourLogRepository.Save();
        }
        public void DeleteTourLog(int tourlogId)
        {
            tourLogRepository.Delete(tourlogId);
            tourLogRepository.Save();
        }
        public void UpdateTourLog(TourLogModel tourlogmodel)
        {
            tourLogRepository.Update(tourlogmodel);
            tourLogRepository.Save();
        }
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return tourLogRepository.GetTourLogs();
        }
        public TourLogModel GetTourLog(int id)
        {
            return tourLogRepository.GetTourLogById(id);
        }
    }
}
