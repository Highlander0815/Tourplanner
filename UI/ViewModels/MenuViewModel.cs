using BLL;
using System;
using System.Windows.Interop;
using System.Windows;
using TourplannerModel;

namespace UI.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ImportExportManager _importExportManager;
        private TourModel _currentTour;

        private RelayCommand? _importCommand = null;
        public RelayCommand ImportCommand => _importCommand ??= new RelayCommand(ImportJSON);

        private RelayCommand? _exportCommand = null;
        public RelayCommand ExportCommand => _exportCommand ??= new RelayCommand(ExportJSON);

        private void ImportJSON()
        {
            _importExportManager.ImportTour();
        }

        private void ExportJSON()
        {
            if (_currentTour != null)
            {
                _importExportManager.ExportTour(_currentTour.Id);
            }
            else
            {
                string msgBoxText = "No tour selected!";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBox.Show(msgBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }

        public MenuViewModel(ImportExportManager importExportManager, SideMenuViewModel sideMenuViewModel)
        {
            _importExportManager = importExportManager;
            sideMenuViewModel.currentTourChangedAction += HandleCurrentTourChange;
        }

        private void HandleCurrentTourChange(TourModel tour)
        {
            _currentTour = tour;
        }
    }
}
