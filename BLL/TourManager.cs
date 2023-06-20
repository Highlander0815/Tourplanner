using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class TourManager
    {
        public Tour Tour { get; set; }
        private Rest _request;
        public TourManager(Tour tour) 
        { 
            this.Tour = tour;
            _request = new Rest(tour);
            _request.Request();
        }
    }
}
