using TourplannerModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourRepository : ITourRepository, IDisposable
    {
        private readonly TourplannerContext context;

        public TourRepository(TourplannerContext context)
        {
            this.context = context;
        }
        public IEnumerable<TourModel> GetTours()
        {
            return context.Tours.ToList();
        }
        public TourModel GetTourById(int tourId)
        {
            return context.Tours.Find(tourId);
        }
        public void Insert(TourModel tour)
        {
            context.Add(tour);
        }
        public void Update(TourModel tour)
        {            
            context.Entry(tour).State = EntityState.Modified;
        }
        public void Delete(int tourId)
        {
            TourModel tour = context.Tours.Find(tourId);
            context.Tours.Remove(tour);
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
    public interface ITourRepository : IDisposable
    {
        IEnumerable<TourModel> GetTours();
        TourModel GetTourById(int tourId);
        void Insert(TourModel tour);
        void Update(TourModel tour);
        void Delete(int tourId);
        void Save();
    }
}
