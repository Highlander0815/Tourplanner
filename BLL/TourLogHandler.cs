using DAL;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using TourplannerModel;

namespace BLL
{
    public class TourLogHandler
    {
        private ITourLogRepository tourLogRepository;
        public TourLogHandler(TourplannerContext tourplannerContext)
        {
            tourLogRepository = new TourLogRepository(tourplannerContext);
        }
        public TourLogHandler()
        {
            tourLogRepository = new TourLogRepository();
        }
        public void AddTourLog(TourLogModel tourlogmodel)
        {
            tourLogRepository.Insert(tourlogmodel);
            tourLogRepository.Save();
            tourLogRepository.Dispose();
        }
        public void DeleteTourLog(int tourlogId)
        {
            tourLogRepository.Delete(tourlogId);
            tourLogRepository.Save();
            tourLogRepository.Dispose();
        }
        public void UpdateTourLog(TourLogModel tourlogmodel)
        {
            tourLogRepository.Update(tourlogmodel);
            tourLogRepository.Save();
            tourLogRepository.Dispose();
        }
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return tourLogRepository.GetTourLogs();
        }
        public IEnumerable<TourLogModel> GetTourLogsById(int tourid)
        {
            return tourLogRepository.GetTourLogsById(tourid);
        }
    }
}
