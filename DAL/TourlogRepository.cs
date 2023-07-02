using TourplannerModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourLogRepository : ITourLogRepository, IDisposable
    {
        private readonly TourplannerContext context;

        public TourLogRepository(TourplannerContext context)
        {
            this.context = context;
        }
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return context.Tourlogs.ToList();
        }
        public TourLogModel GetTourLogById(int tourLogId)
        {
            return context.Tourlogs.Find(tourLogId);        
        }
        public void Insert(TourLogModel tourlog)
        {
            context.Add(tourlog);
        }
        public void Delete(int tourLogId)
        {
            TourLogModel tourLog = context.Tourlogs.Find(tourLogId);
            context.Tourlogs.Remove(tourLog);
        }
        public void Update(TourLogModel tourlog)
        {
            context.Entry(tourlog).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public interface ITourLogRepository
    {
        IEnumerable<TourLogModel> GetTourLogs();
        TourLogModel GetTourLogById(int tourLogId);
        void Insert(TourLogModel tourlog);
        void Update(TourLogModel tourlog);
        void Delete(int tourLogId);
        void Save();
    }
}
