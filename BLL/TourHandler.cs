using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using TourplannerModel;

namespace BLL
{
    public class TourHandler
    {
        private ITourRepository tourRepository;
        public TourHandler(IConfiguration configuration)
        {
            tourRepository = new TourRepository(new TourplannerContext(configuration));
        }
                
    }
}
