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

        public event Action<TourLogModel> AddEvent; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(AddTourLog);

        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();

        private TourLogHandler _tourLogHandler;
        private SideMenuViewModel _sideMenuViewModel;

        private TourModel _currentTour;
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private TimeOnly _time;
        public TimeOnly Time
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

        public AddTourLogViewModel(TourLogHandler tourLogHandler, SideMenuViewModel sideMenuViewModel)
        {
            _tourLogHandler = tourLogHandler;
            _sideMenuViewModel = sideMenuViewModel;
            _currentTour = sideMenuViewModel.CurrentTour;
        }

        private async void AddTourLog()
        {
            TourLogModel tourLog = new TourLogModel(DateOnly.FromDateTime(_date), _time, _difficulty, _totalTime, _rating, _currentTour);
            _tourLogHandler.AddTourLog(tourLog);
            this.AddEvent?.Invoke(tourLog);
        }

        private void Cancel()
        {
            this.CancelEvent?.Invoke(this, EventArgs.Empty);
        }

        public void SetValues()
        {
            //Date = DateOnly.FromDateTime(DateTime.Today);
            Date = DateTime.Now;
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Difficulty = DifficultyEnum.Advance;
        }
    }
}
