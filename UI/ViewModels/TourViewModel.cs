using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace UI.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private readonly Tour _tour;
        public string Name => _tour.Name;
        public string Description => _tour.Description;
        public string From => _tour.From;
        public string To => _tour.To;
        public string TransportType => _tour.TransportType;

        public TourViewModel(Tour tour)
        {
            _tour = tour;
        }
    }
}
