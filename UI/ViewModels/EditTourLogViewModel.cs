using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;

namespace UI.ViewModels
{
    public class EditTourLogViewModel : ViewModelBase
    {
        public event Action<TourLogModel> SubmitAction; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(EditTour);

        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                OnPropertyChanged(nameof(DateTime));
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
        public EditTourLogViewModel()
        {
            DateTime = DateTime.Now;
        }

        private async void EditTour()
        {
            TourLogModel tourLog = new TourLogModel(_dateTime, _difficulty, _totalTime, _rating);
      
            SubmitAction?.Invoke(tourLog);
        }

        private void Cancel()
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
