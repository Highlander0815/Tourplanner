using TourplannerModel;

namespace BLL
{
    public class TourManager
    {
        public TourModel Tour { get; set; }
        public Rest Rest { get; set; }  
        public TourManager(TourModel tour) 
        { 
            this.Tour = tour;
            this.Rest = new Rest();
        }
    }
}
