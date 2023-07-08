using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;
using UI.Views;

namespace UI.ViewModels
{
    public class AddTourLogViewModel : ViewModelBase
    {

        public event Action AddEvent; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private Validator _validator;
        private TourLogModel _newTourLog;

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(AddTourLog);

        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();

        //private InMemoryTourLogHandler _tourLogHandler; //mit dem TourHandler crash das programm ich nehme mal an der ist nur für die unit tests da aber das kann denk ich nicht im production code stehen 
        private TourLogHandler _tourLogHandler;
        private SideMenuViewModel _sideMenuViewModel;

        private TourModel _currentTour;

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }
        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                OnPropertyChanged(nameof(DateTime));
                UpdateButtonState();
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
                UpdateButtonState();
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
                UpdateButtonState();
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
                UpdateButtonState();
            }
        }

        public AddTourLogViewModel(/*InMemoryTourLogHandler tourLogHandler,*/TourLogHandler tourLogHandler, SideMenuViewModel sideMenuViewModel)
        {
            _tourLogHandler = tourLogHandler;
            _sideMenuViewModel = sideMenuViewModel;
            _currentTour = sideMenuViewModel.CurrentTour;
        }
        private void UpdateButtonState()
        {
            _newTourLog = new TourLogModel(_dateTime, _difficulty, _totalTime, _rating);
            _validator = new BLL.Validator();
            bool allFieldsFilled = _validator.TourLogValidation(_newTourLog);

            IsButtonEnabled = allFieldsFilled;
        }
        private void AddTourLog()
        {
            IsButtonEnabled = false;
            TourLogModel tourLog = new TourLogModel(_dateTime, _difficulty, _totalTime, _rating, _currentTour);
            _tourLogHandler.AddTourLog(tourLog);
            this.AddEvent?.Invoke();
        }

        private void Cancel()
        {
            this.CancelEvent?.Invoke(this, EventArgs.Empty);
        }

        public void SetValues()
        {
            DateTime = DateTime.Now;
        }
    }
}
