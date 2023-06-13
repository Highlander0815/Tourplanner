﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace UI.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private readonly Tour _tour;
        private string Name => _tour.Name;
        private string Description => _tour.Description;
        private string From => _tour.From;
        private string To => _tour.To;
        private string TransportType => _tour.TransportType;

        public TourViewModel(Tour tour)
        {
            _tour = tour;
        }
    }
}
