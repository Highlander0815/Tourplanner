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
using BLL;
using System.Windows;
using Microsoft.Win32;

namespace UI.ViewModels
{
    public class CenterWindowViewModel : ViewModelBase
    {
        public Action<TourModel> currentTourChangedAction;
        private DisplayInfoViewModel _displayInfoViewModel;
        private DisplayRouteViewModel _displayRouteViewModel;
        private TourModel _currentTour;
        private PDFManager _pdfManager;

        public CenterWindowViewModel(SideMenuViewModel sideMenuViewModel, DisplayInfoViewModel displayInfoViewModel, DisplayRouteViewModel displayRouteViewModel, PDFManager pdfManager)
        {
            sideMenuViewModel.currentTourChangedAction += HandleCurrentTourChange;
            _displayInfoViewModel = displayInfoViewModel;
            _displayRouteViewModel = displayRouteViewModel;
            _pdfManager = pdfManager;
        }

        private void HandleCurrentTourChange(TourModel tour)
        {

            _currentTour = tour;
            _displayInfoViewModel.currentTour = _currentTour;
            _displayRouteViewModel.currentTour = _currentTour;

            _displayInfoViewModel.UpdateInfoView();
            _displayRouteViewModel.UpdateRouteView();
        }                


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
        private RelayCommand? _savePDFCommand = null;
        public RelayCommand SavePDFCommand => _savePDFCommand ??= new RelayCommand(SavePDF);

        private void SavePDF()
        {
            if (_currentTour != null)
            {
                bool result = _pdfManager.createPDF(_currentTour.Id);
                if (result)
                {
                    MessageBoxImage icon = MessageBoxImage.Information;
                    ShowMessageBox("pdf created successfully!", "Information", icon);
                }
                else
                {
                    MessageBoxImage icon = MessageBoxImage.Error;
                    ShowMessageBox("pdf creation aborted!", "Error", icon);
                }                
            }
            else
            {
                MessageBoxImage icon = MessageBoxImage.Warning;
                ShowMessageBox("No tour selected!", "Warning", icon);
            }            
        }
        public RelayCommand InfoCommand => _infoCommand ??= new RelayCommand(DisplayInfoView);
        
        private RelayCommand? _routeCommand = null;
        public RelayCommand RouteCommand => _routeCommand ??= new RelayCommand(DisplayRouteView);
        //private RelayCommand? _miscCommand = null;


        //private Methods
        private void DisplayInfoView()
        {
            CurrentContent = new DisplayInfoWindow();//_displayInfoViewModel.GetInfoView();
        }
        
        private void ShowMessageBox(string msg, string caption, MessageBoxImage icon)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result;

            result = MessageBox.Show(msg, caption, button, icon, MessageBoxResult.OK);
        }

        private void DisplayRouteView()
        {
            CurrentContent = new DisplayRouteWindow();//_displayRouteViewModel.GetRouteView();

        }
    }
}
