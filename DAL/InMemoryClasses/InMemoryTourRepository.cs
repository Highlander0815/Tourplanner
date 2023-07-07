using TourplannerModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel;

namespace DAL
{
    public class InMemoryTourRepository : IInMemoryTourRepository
    {
        private ObservableCollection<TourModel> _tours;
        public InMemoryTourRepository()
        {
            _tours = new ObservableCollection<TourModel>();
        }

        public IEnumerable<TourModel> GetTours()
        {
            return _tours.ToList();
        }
        public TourModel GetTourById(int tourId)
        {
            return _tours.Where(t => t.Id.Equals(tourId)).Single();
        }
        public void Insert(TourModel tour)
        {
            _tours.Add(tour);
        }
        public void Update(TourModel tour)
        {
            // Nothing to test here
        }
        public void Delete(TourModel tour)
        {
            if (tour != null)
            {
                _tours.Remove(tour);
            }
            else
            {
                throw new Exception("No tour with matching id");
            }
        }
        public void Save()
        {
            // nothing to test here
        }
    }
    public interface IInMemoryTourRepository
    {
        IEnumerable<TourModel> GetTours();
        TourModel GetTourById(int tourId);
        void Insert(TourModel tour);
        void Update(TourModel tour);
        void Delete(TourModel tour);
        void Save();
    }
}
