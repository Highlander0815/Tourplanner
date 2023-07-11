using BLL;
using System;
using System.Windows.Interop;
using System.Windows;
using TourplannerModel;
using BLL.Exceptions;

namespace UI.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ImportExportManager _importExportManager;
        private TourModel _currentTour;
        private BottomMenuViewModel _bottomMenuViewModel;
        private SideMenuViewModel _sideMenuViewModel;

        private RelayCommand? _importCommand = null;
        public RelayCommand ImportCommand => _importCommand ??= new RelayCommand(ImportJSON);

        private RelayCommand? _exportCommand = null;
        public RelayCommand ExportCommand => _exportCommand ??= new RelayCommand(ExportJSON);

        private void ImportJSON()
        {
            try
            {
                _importExportManager.ImportTour();
                _logger.Info("A Tour got imported successfully");
                _sideMenuViewModel.UpdateList();
            }
            catch (Exception ResponseErrorOfApiException)
            {
                ShowMessageBox($"{ResponseErrorOfApiException.Message} Please check your input in the to and from field");
                _logger.Error($"The Imput from 'To' and/or 'From' could not be handeled from the API.");
            }
            catch
            {
                ShowMessageBox("A new Error occured with should be handeled");
            }
        }

        private void ExportJSON()
        {
            if (_currentTour != null)
            {
                _importExportManager.ExportTour(_currentTour.Id);
                _logger.Info("A Tour got exported successfully");
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

        public MenuViewModel(ImportExportManager importExportManager, SideMenuViewModel sideMenuViewModel, BottomMenuViewModel bottomMenuViewModel)
        {
            _importExportManager = importExportManager;
            _sideMenuViewModel = sideMenuViewModel;
            _bottomMenuViewModel = bottomMenuViewModel;
            sideMenuViewModel.currentTourChangedAction += HandleCurrentTourChange;
        }

        private void HandleCurrentTourChange(TourModel tour)
        {
            _currentTour = tour;
        }
    }
}
