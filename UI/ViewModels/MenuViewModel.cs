using BLL;
using System;
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
            _importExportManager.ExportTour(_currentTour.Id);
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
