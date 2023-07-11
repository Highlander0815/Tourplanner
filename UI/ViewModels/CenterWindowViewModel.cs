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
using BLL.Logging;
using BLL.Exceptions;

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
        private RelayCommand? _saveTourReportPDFCommand = null; 
        public RelayCommand SaveTourReportPDFCommand => _saveTourReportPDFCommand ??= new RelayCommand(SaveTourReportPDF);
        
        private RelayCommand? _saveSummaryPDFCommand = null; 
        public RelayCommand SaveSummaryPDFCommand => _saveSummaryPDFCommand ??= new RelayCommand(SaveSummaryPDF);

        private void SaveTourReportPDF()
        {
            if (_currentTour != null)
            {
                bool result = _pdfManager.createTourReportPDF(_currentTour);
                if (result)
                {
                    MessageBoxImage icon = MessageBoxImage.Information;
                    ShowMessageBox("pdf created successfully!", "Information", icon);
                    _logger.Info("The user created a Pdf for the Tour with the Id: " + _currentTour.Id);

                }
                else
                {
                    MessageBoxImage icon = MessageBoxImage.Error;
                    ShowMessageBox("pdf creation aborted!", "Error", icon);
                    _logger.Error("The Pdf creation for the Tour " + _currentTour.Id+ " failed");
                }                
            }
            else
            {
                MessageBoxImage icon = MessageBoxImage.Warning;
                ShowMessageBox("No tour selected!", "Warning", icon);
                _logger.Warn("User did not select a Tour, for wich one the Pdf should be created");
            }            
        }
        private void SaveSummaryPDF()
        {
            try
            {
                bool result = _pdfManager.createSummaryPDF();
                if (result)
                {
                    MessageBoxImage icon = MessageBoxImage.Information;
                    ShowMessageBox("pdf Summary created successfully!", "Information", icon);
                    _logger.Info("The user created a Summary Pdf");

                }
                else
                {
                    MessageBoxImage icon = MessageBoxImage.Error;
                    ShowMessageBox("pdf Summary creation aborted!", "Error", icon);
                    _logger.Error("The Summary Pdf creation failed");
                }
            }
            catch (NoToursException ex) 
            {
                MessageBoxImage icon = MessageBoxImage.Information;
                ShowMessageBox("There are no tours at the moment to generate a summary!", "Information", icon);
                _logger.Info("The Summary Pdf creation failed because at the moment there are no tours");
            }
            catch
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                ShowMessageBox("In CenterWindowViewModel was a new Exception thrown which should be handeled", "Error", icon);
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
        
       

        private void DisplayRouteView()
        {
            CurrentContent = new DisplayRouteWindow();//_displayRouteViewModel.GetRouteView();

        }
    }
}
