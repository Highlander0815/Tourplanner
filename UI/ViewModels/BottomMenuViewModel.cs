using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TourplannerModel;

namespace UI.ViewModels
{
    public class BottomMenuViewModel : ViewModelBase
    {

        public BottomMenuViewModel(SideMenuViewModel sideMenuViewModel)
        {
            _currentTour = null;
            currentTourLogChangedAction += HandleTourLogChange;
            sideMenuViewModel.currentTourChangedAction += HandleTourChanged;
        }


        //Actions
        public event Action<TourLogModel> currentTourLogChangedAction;

        private TourModel _currentTour; //has to be saved because the currentTour contains the TourLogs which should be displayed

        //List of TourLogs
        private ObservableCollection<TourLogModel> _tourLogs;
        public ObservableCollection<TourLogModel> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        //current TourLog = Selected item in the dataGrid. Is needed to know the currentTourLog when you want to edit/delete it.
        private TourLogModel _currentTourLog;
        public TourLogModel CurrentTourLog
        {
            get
            {
                return _currentTourLog;
            }
            set
            {
                if (_currentTourLog != value)
                {
                    _currentTourLog = value;
                    OnPropertyChanged(nameof(CurrentTourLog));
                }
            }
        }
        // Difficulty-Optionen
        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        // Rating-Optionen
        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();

        //Events
        public event EventHandler OpenAddTourLog;
        public event EventHandler OpenEditTourLog;

        //Commands
        private RelayCommand? _addCommand = null;
        private RelayCommand? _editCommand = null;
        private RelayCommand? _deleteCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(OpenAddTourLogW);
        public RelayCommand EditCommand => _editCommand ??= new RelayCommand(OpenEditTourLogW);
        public RelayCommand DeleteCommand => _deleteCommand ??= new RelayCommand(Delete);


        //private Methods
        private void OpenAddTourLogW()
        {
            if(_currentTour != null) //otherwise the new TourLog can not be added to a Tour
                this.OpenAddTourLog?.Invoke(this, EventArgs.Empty);
        }

        private void OpenEditTourLogW()
        {
            if(_currentTourLog != null) //if no tourLog is selected their is nothing which can be edited
            {
                this.OpenEditTourLog?.Invoke(this, EventArgs.Empty);
            }
            
        }

        private void Delete() 
        {
            if (_tourLogs.Contains(_currentTourLog))
            {
                TourLogs.Remove(_currentTourLog);
            }
            else
            {
                //noch implementieren //ka ob ma das überhaupt brauchen ich denke eigentlich müsste der TourLog immer Enthalten sein
            }
        }

        public void UpdateList(TourLogModel tourLog)
        {
            if (_tourLogs.Contains(_currentTourLog))
            {
                _tourLogs[_tourLogs.IndexOf(_currentTourLog)] = tourLog;
            }
            else
            {
                //noch ka was dann passiert
            }
        }

        private void HandleTourLogChange(TourLogModel tourLog)
        {
            CurrentTourLog = tourLog; //When the CurrentTourLog changes the CurrentTourLog has to be updated
        }

        private void HandleTourChanged(TourModel tour) //when a Tour gets selected in the SideMenu the TourLogs which are displayed in the BottomMenu have to be displayed.
        {
            if(tour != null)
            {
                _currentTour = tour;
                TourLogs = tour.TourLogs;
                _currentTourLog = null;
            }
        }
    }
}
