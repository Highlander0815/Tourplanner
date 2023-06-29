using TourplannerModel;
using MiNET.Entities.Behaviors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UI.Views;

namespace UI.ViewModels
{
    public class CenterWindowViewModel : ViewModelBase
    {
        public CenterWindowViewModel(SideMenuViewModel sideMenuViewModel, DisplayInfoViewModel displayInfoViewModel, DisplayRouteViewModel displayRouteViewModel)
        {
            sideMenuViewModel.currentTourChangedAction += HandleCurrentTourChange;
            _displayInfoViewModel = displayInfoViewModel;
            _displayRouteViewModel = displayRouteViewModel;
            
        }

        private void HandleCurrentTourChange(TourModel tour)
        {
            if(tour != null)
            {
                currentTour = tour;
                _displayInfoViewModel.currentTour = currentTour;
                _displayRouteViewModel.currentTour = currentTour;

                _displayInfoViewModel.GetInfoView();
                _displayRouteViewModel.GetRouteView();
            }
           
        }

        public Action<TourModel> currentTourChangedAction;
        private DisplayInfoViewModel _displayInfoViewModel;
        private DisplayRouteViewModel _displayRouteViewModel;
        public TourModel currentTour;
        private object _currentContent;
        public object CurrentContent
        {
            get { return _currentContent; }
            set
            {
                _currentContent = value;
                OnPropertyChanged(nameof(CurrentContent));
            }
        }

        //Commands
        private RelayCommand? _infoCommand = null;
        public RelayCommand InfoCommand => _infoCommand ??= new RelayCommand(DisplayInfoView);
        
        private RelayCommand? _routeCommand = null;
        public RelayCommand RouteCommand => _routeCommand ??= new RelayCommand(DisplayRouteView);
        //private RelayCommand? _miscCommand = null;


        //private Methods
        private void DisplayInfoView()
        {
            CurrentContent = new DisplayInfoWindow();//_displayInfoViewModel.GetInfoView();
        }

        private void DisplayRouteView()
        {
            CurrentContent = new DisplayRouteWindow();//_displayRouteViewModel.GetRouteView();

        }
    }
}
