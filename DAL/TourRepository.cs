using TourplannerModel;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourRepository : ITourRepository, IDisposable
    {
        private readonly TourplannerContext _context;

        public TourRepository(TourplannerContext context)
        {
            _context = context;
 
        }
        public IEnumerable<TourModel> GetTours()
        {
            return _context.Tours.ToList();
        }
        public TourModel GetTourById(int tourId)
        {
            return _context.Tours.Find(tourId);
        }
        public void Insert(TourModel tour)
        {
            _context.Add(tour);
        }
        public void Update(TourModel tour)
        {
            _context.Entry(tour).State = EntityState.Modified;
        }
        public void Delete(int tourId)
        {
            TourModel tour = _context.Tours.Find(tourId);
            _context.Tours.Remove(tour);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
