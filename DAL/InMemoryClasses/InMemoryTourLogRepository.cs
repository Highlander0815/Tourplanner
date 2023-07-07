using TourplannerModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DAL
{
    public class InMemoryTourLogRepository : IInMemoryTourLogRepository
    {
        private ObservableCollection<TourLogModel> _tourlogs;
        public InMemoryTourLogRepository()
        {
            _tourlogs = new ObservableCollection<TourLogModel>();
        }
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return _tourlogs.ToList();
        }
        public IEnumerable<TourLogModel> GetTourLogsById(int tourid)
        {
            return _tourlogs.Where(t => t.TourModel.Id.Equals(tourid)).ToList();
        }
        public void Insert(TourLogModel tourlog)
        {
            _tourlogs.Append(tourlog);
        }
        public void Delete(TourLogModel tourLog)
        {    
            if (tourLog != null)
            {
                _tourlogs.Remove(tourLog);
            }
            else
            {
                throw new Exception("No tourlog with matching id");
            }
        }
        public void Update(TourLogModel tourlog)
        {
            // Nothing to test here
        }
        public void Save()
        {
            // nothing to test here
        }
    }
    public interface IInMemoryTourLogRepository
    {
        IEnumerable<TourLogModel> GetTourLogs();
        IEnumerable<TourLogModel> GetTourLogsById(int tourid);
        void Insert(TourLogModel tourlog);
        void Update(TourLogModel tourlog);
        void Delete(TourLogModel tourLog);
        void Save();
    }
}
