using DAL;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using TourplannerModel;

namespace BLL
{
    public class InMemoryTourLogHandler
    {
        private InMemoryTourLogRepository _tourLogRepository;
         public InMemoryTourLogHandler()
        {
            _tourLogRepository = new InMemoryTourLogRepository();
        }
        public void AddTourLog(TourLogModel tourlogmodel)
        {
            _tourLogRepository.Insert(tourlogmodel);
        }
        public void DeleteTourLog(TourLogModel tourlog)
        {
            _tourLogRepository.Delete(tourlog);
        }
        public void UpdateTourLog(TourLogModel tourlogmodel)
        {
            // No test for this method
        }
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return _tourLogRepository.GetTourLogs();
        }
        public IEnumerable<TourLogModel> GetTourLogsById(int tourid)
        {
            return _tourLogRepository.GetTourLogsById(tourid);
        }
    }
}
