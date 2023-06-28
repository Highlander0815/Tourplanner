using BLL.Models;

namespace BLL
{
    public class TourManager
    {
        public Tour Tour { get; set; }
        public Rest Rest { get; set; }  
        public TourManager(Tour tour) 
        { 
            this.Tour = tour;
            this.Rest = new Rest();
        }
    }
}
