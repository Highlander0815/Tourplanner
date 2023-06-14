using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL.Models;
using Microsoft.VisualBasic;
using UI.Views;

namespace UI.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        public event EventHandler OpenAddTourW;
        private readonly ObservableCollection<TourViewModel> _tours;
        public IEnumerable<TourViewModel>  Tours => _tours;


        //Commands
        private RelayCommand _addCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(AddTour);
        public ICommand ModifyTour { get; }
        public ICommand DeleteTour { get; }


        public SideMenuViewModel()
        {
            RelayCommand test = new RelayCommand(OpenAddTourWC);
            
            _tours = new ObservableCollection<TourViewModel>();

            _tours.Add(new TourViewModel(new Tour("test", "test", "test", "test", "test")));
            _tours.Add(new TourViewModel(new Tour("test1", "test1", "test1", "test1", "test1")));
        }
        public void OpenAddTourWC()
        {
            this.OpenAddTourW?.Invoke(this, EventArgs.Empty);
        }
        public void Add(TourViewModel tour)
        {
            _tours.Add(tour);
        }

        private void AddTour()
        {
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.ShowDialog();
        }
    }
}
