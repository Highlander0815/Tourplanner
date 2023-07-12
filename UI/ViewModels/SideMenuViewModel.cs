using BLL;
using BLL.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using TourplannerModel;

namespace UI.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        //Actions
        public event Action<TourModel> currentTourChangedAction;

        //Events
        public event EventHandler OpenAddTour;
        public event EventHandler OpenEditTour;

        //Commands
        private RelayCommand? _addCommand = null;
        private RelayCommand? _editCommand = null;
        private RelayCommand? _deleteCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(OpenAddTourW);
        public RelayCommand EditCommand => _editCommand ??= new RelayCommand(OpenEditTourW);
        public RelayCommand DeleteCommand => _deleteCommand ??= new RelayCommand(Delete);

        //Attributes
        private ObservableCollection<TourModel> _tours { get; set; }
        public ObservableCollection<TourModel> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(VisibleTours));
            }
        }
        public IEnumerable<TourModel> VisibleTours => _tours.Where(tour => tour.Visible);
        public void UpdateView()
        {
            OnPropertyChanged(nameof(Tours));
            OnPropertyChanged(nameof(VisibleTours));
        }
        private TourModel _currentTour;
        public TourModel CurrentTour {
            get
            {
                return _currentTour;
            }
            set 
            {
                if (_currentTour != value)
                {
                    _currentTour = value;
                    currentTourChangedAction?.Invoke(_currentTour);
                }
            } 
        }


        private readonly TourHandler _tourHandler;
        private readonly SearchbarViewModel _searchbarViewModel;
        //Constructor
        public SideMenuViewModel(TourHandler tourHandler)
        {
            Tours = new ObservableCollection<TourModel>();
            _tourHandler = tourHandler;

            //Retrieve existing Tours from db and display in SideMenu
            Tours = new ObservableCollection<TourModel>(tourHandler.GetTours());
        }

        //private Methods
        private void OpenAddTourW()
        {
            _logger.Info("Add Tour Window got opened");
            OpenAddTour?.Invoke(this, EventArgs.Empty);
        }

        private void OpenEditTourW()
        {
            if(_currentTour != null) 
            {
                //string path = _currentTour.Image;
                //_tourHandler.UpdateTour(_currentTour);
                _logger.Info("Edit Tour Window got oppened");
                OpenEditTour?.Invoke(this, EventArgs.Empty);
                //File.Delete(path);
                currentTourChangedAction?.Invoke(CurrentTour);
                if(_currentTour != null)
                    _logger.Info("The Tour with the Id: " + _currentTour.Id + " was modified");

            }
            else
            {
                ShowMessageBox("No tour selected");
                _logger.Warn("It is not possible to modify a tour when no tour is selected");
            }
        }

        private void Delete()
        {
            if (_currentTour != null)
            {
                string pathOfCurrentTour = _currentTour.Image;
                _tourHandler.DeleteTour(_currentTour.Id);
                _logger.Info($"The tour with the Id: {_currentTour.Id} was deleted");
                Tours.Remove(_currentTour);
                OnPropertyChanged(nameof(VisibleTours));
                if(!string.IsNullOrEmpty(pathOfCurrentTour))
                    File.Delete(pathOfCurrentTour);
            }
            else
            {
                ShowMessageBox("No tour selected");
                _logger.Warn("It is not possible to delete a tour when no tour is selected");
            }
        }

        //public Methods
        public void Save(TourModel tour)
        {
            Tours.Add(tour);
            OnPropertyChanged(nameof(VisibleTours));
            _logger.Info("Added tour " + tour.Id);
        }
        public void UpdateList(/*TourModel tour*/)
        {
            Tours = new ObservableCollection<TourModel>(_tourHandler.GetTours());
            _logger.Info("The List of tours got updated");
        }
        
        public void TriggerCurrentTourChangedAction(TourModel tour) //this method is needed to trigger the CurrentTourChangedAction when deleting/adding/editing TourLogs 
        {                                                           //because this leads to a update of the DisplayInfoViewModel
            if (tour != null)
            {
                currentTourChangedAction?.Invoke(tour);
            }
        }
    }
}
