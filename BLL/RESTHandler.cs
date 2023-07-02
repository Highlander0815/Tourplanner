using TourplannerModel;

namespace BLL
{
    public class RESTHandler
    {
        public Rest Rest { get; set; }  
        public RESTHandler()
        { 
            this.Rest = new Rest();
        }
    }
}
