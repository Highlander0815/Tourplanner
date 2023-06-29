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
            currentTourLogChangedAction += HandleTourLogChange;
            sideMenuViewModel.currentTourChangedAction += HandleTourChanged;
        }

        private void HandleTourLogChange(TourLogModel tourLog)
        {
            CurrentTourLog = tourLog;  
        }

        //Actions
        public event Action<TourLogModel> currentTourLogChangedAction;
        private void HandleTourChanged(TourModel tour)
        {

            _currentTour = tour;
            TourLogs = tour.TourLogs;

        }

        private TourModel _currentTour;
        private Action<TourModel> _currentTourChangedAction; 

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

        // Difficulty-Optionen
        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        // Rating-Optionen
        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();
        /* private bool _isRowExpanded;
         public bool IsRowExpanded
         {
             get { return _isRowExpanded; }
             set
             {
                 _isRowExpanded = value;
                 OnPropertyChanged(nameof(IsRowExpanded));
             }
         }*/
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
                    //currentTourLogChangedAction?.Invoke(_currentTourLog);
                }
            }
        }

        private DateOnly _date;
        public DateOnly Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private DifficultyEnum _difficulty;
        public DifficultyEnum Difficulty
        {
            get { return _difficulty; }
            set
            {
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        private TimeSpan _totalTime;
        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set
            {
                _totalTime = value;
                OnPropertyChanged(nameof(TotalTime));
            }
        }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        private RelayCommand _addTourLog= null;
        public RelayCommand AddTourLog => _addTourLog ??= new RelayCommand(AddLog);

        private RelayCommand _editTourLog = null;
        public RelayCommand EditTourLog => _editTourLog ??= new RelayCommand(EditLog);
       /* private KeyEventHandler<TourLogModel> _previewKeyDownCommand += new KeyEventHandler<TourLogModel>(HandleEnter);
        public KeyEventHandler PreviewKeyDownCommand => _previewKeyDownCommand ??= new RelayCommand<KeyEventArgs>(HandlePreviewKeyDown);*/
       //public event DataGrid_PreviewKeyDown;

        /*void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Überprüfe, ob die Eingabezeile aktiv ist
                var dataGrid = (DataGrid)sender;
                var editingElement = dataGrid?.GetFocusElement();
                if (editingElement != null && editingElement.GetType() == typeof(DataGridCell))
                {
                    if (!checkData())
                    {
                        TourLogs.Remove(_currentTourLog);
                    }
                    // Füge hier deine gewünschte Logik hinzu
                    // z.B. Validierung und Speichern der Daten
                    // ...

                    // Setze den Fokus zurück auf das DataGrid, um die Bearbeitung abzuschließen
                    dataGrid.Focus();
                    e.Handled = true; // Behandelt das Ereignis, um ein zusätzliches "Enter"-Ereignis zu vermeiden
                }
            }
        }*/
        private void AddLog()
        {
            //_currentTourLog = new TourLogModel(DifficultyEnum.Beginner, TimeSpan.Zero, 1);
            TourLogs.Add(_currentTourLog);
            //IsRowExpanded = true;
        }
        private void EditLog()
        {
            throw new NotImplementedException();
        }

        /*private void HandlePreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Überprüfe, ob die Eingabezeile aktiv ist
                var dataGrid = e.Source as DataGrid;
                var editingElement = dataGrid?.GetFocusElement();
                if (editingElement != null && editingElement.GetType() == typeof(DataGridCell))
                {
                    if (!checkData())
                    {
                        TourLogs.Remove(_currentTourLog);
                    }
                    // Füge hier deine gewünschte Logik hinzu
                    // z.B. Validierung und Speichern der Daten
                    // ...

                    // Setze den Fokus zurück auf das DataGrid, um die Bearbeitung abzuschließen
                    dataGrid.Focus();
                    e.Handled = true; // Behandelt das Ereignis, um ein zusätzliches "Enter"-Ereignis zu vermeiden
                }
            }
        }
       */

        private bool checkData()
        {
            if (_currentTourLog.Date == null || _currentTourLog.Time == null || _currentTourLog.Difficulty == null || _currentTourLog.TotalTime == null || _currentTourLog.Rating == null)
                return false;

            return true;
        }
    }
}
