using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Models;

namespace UI.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        private readonly ObservableCollection<TourViewModel> _tours;
        public IEnumerable<TourViewModel>  Tours => _tours;

        public ICommand AddTour { get; }
        public ICommand ModifyTour { get; }
        public ICommand DeleteTour { get; }


        public SideMenuViewModel()
        {
            _tours = new ObservableCollection<TourViewModel>();

            _tours.Add(new TourViewModel(new Tour("test", "test", "test", "test", "test")));
        }
    }
}
