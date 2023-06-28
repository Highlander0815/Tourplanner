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
        public Rest Rest { get; set; }  
        public TourManager(Tour tour) 
        { 
            this.Tour = tour;
            this.Rest = new Rest();
        }
    }
}
