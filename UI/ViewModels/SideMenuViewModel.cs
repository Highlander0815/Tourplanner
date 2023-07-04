using BLL;
using System;
using System.Collections.ObjectModel;
using System.IO;
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
        private ObservableCollection<TourModel> _tours;
        public ObservableCollection<TourModel> Tours
        {
            get { return _tours; }
            set 
            { 
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }
        
        //Tour
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

        //Constructor
        private TourHandler _tourHandler;
        public SideMenuViewModel(TourHandler tourHandler)
        {
            _tours = new ObservableCollection<TourModel>();

            _tourHandler = tourHandler;
            
            //Retrieve existing Tours from db and display in SideMenu
            _tours = new ObservableCollection<TourModel>(tourHandler.GetTours());
        }

        //private Methods
        private void OpenAddTourW()
        {
            this.OpenAddTour?.Invoke(this, EventArgs.Empty);
        }
        private void Add(TourModel tour)
        {
            _tours.Add(tour);
        }

        private void OpenEditTourW()
        {
            if(_currentTour != null) 
            {
                //string path = _currentTour.Image;
                this.OpenEditTour?.Invoke(this, EventArgs.Empty);
                //File.Delete(path);
                currentTourChangedAction?.Invoke(_currentTour);
            }
            else
            {
                ShowMessageBox("No tour selected");
            }
        }

        private void Delete()
        {
            if (_currentTour != null)
            {
                string pathOfCurrentTour = _currentTour.Image;
                _tours.Remove(_currentTour);
                //CurrentTour = null;
                File.Delete(pathOfCurrentTour);
            }
            else
            {
                ShowMessageBox("No tour selected");
            }
        }

        //public Methods
        public void Save(TourModel tour)
        {
            Tours.Add(tour);
        }
        public void UpdateList(TourModel tour)
        {
            int index = Tours.IndexOf(tour);
            Tours.RemoveAt(index); //removing and adding the tour again triggers the OnPropertyChange event of the List which automatically updates the List and so also the Name displayed in the List
            Tours.Insert(index, tour);
            CurrentTour = tour;
        }
        private void ShowMessageBox(string msg)
        {
            string msgBoxText = msg;
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(msgBoxText, caption, button, icon, MessageBoxResult.OK);
        }
    }
}
