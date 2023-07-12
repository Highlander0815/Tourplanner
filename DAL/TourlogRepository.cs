using TourplannerModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourLogRepository : ITourLogRepository
    {
        private readonly TourplannerContext context;

        public TourLogRepository(TourplannerContext context)
        {
            this.context = context;
        }
        public TourLogRepository() 
        {
            context = new TourplannerContext();
        }  
        public IEnumerable<TourLogModel> GetTourLogs()
        {
            return context.Tourlogs.ToList();
        }
        public IEnumerable<TourLogModel> GetTourLogsById(int tourid)
        {
            return context.Tourlogs.Where(t => t.TourModelId.Equals(tourid)).ToList();
        }
        public void Insert(TourLogModel tourlog)
        {
            context.Add(tourlog);
        }
        public void Delete(int tourLogId)
        {
            TourLogModel tourLog = context.Tourlogs.Find(tourLogId);

            if (tourLog != null)
            {
                context.Tourlogs.Remove(tourLog);
            }
            else
            {
                throw new Exception("No tourlog with matching id");
            }
        }
        public void Update(TourLogModel tourlog)
        {
            context.Entry(tourlog).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
            context.Dispose();
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
    public interface ITourLogRepository : IDisposable
    {
        IEnumerable<TourLogModel> GetTourLogs();
        IEnumerable<TourLogModel> GetTourLogsById(int tourid);
        void Insert(TourLogModel tourlog);
        void Update(TourLogModel tourlog);
        void Delete(int tourLogId);
        void Save();
    }
}
