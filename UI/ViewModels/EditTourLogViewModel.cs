﻿using BLL;
using MiNET.UI;
using MiNET.Utils.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;

namespace UI.ViewModels
{
    public class EditTourLogViewModel : ViewModelBase
    {
        public event Action SubmitAction; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private Validator _validator;
        private TourLogModel _newTourLog;
        private SideMenuViewModel _sideMenuViewModel;

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(EditTour);

        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        public List<DifficultyEnum> DifficultyOptions { get; } = Enum.GetValues(typeof(DifficultyEnum)).Cast<DifficultyEnum>().ToList();

        public List<int> RatingOptions { get; } = Enumerable.Range(1, 5).ToList();
       
        private BottomMenuViewModel _bottomMenuViewModel;

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

        private TimeOnly _time;
        public TimeOnly Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
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
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
                UpdateButtonState();
            }
        }
        public EditTourLogViewModel(BottomMenuViewModel bottomMenuViewModel, SideMenuViewModel sideMenuViewModel)
        {
            _sideMenuViewModel = sideMenuViewModel;
            _bottomMenuViewModel = bottomMenuViewModel;
            DateTime = DateTime.Now;
        }
        private void UpdateButtonState()
        {
            _newTourLog = new TourLogModel(_dateTime, _difficulty, _totalTime, _rating, _comment);
            _validator = new BLL.Validator();
            bool allFieldsFilled = _validator.TourLogValidation(_newTourLog);

            IsButtonEnabled = allFieldsFilled;
        }
        private async void EditTour()
        {
            IsButtonEnabled = false;
            TourLogModel currentTourLog = _bottomMenuViewModel.CurrentTourLog;
            currentTourLog.DateTime = _dateTime;
            currentTourLog.Difficulty = _difficulty;
            currentTourLog.TotalTime = _totalTime;
            currentTourLog.Rating = _rating;
            currentTourLog.Comment = _comment;
            _sideMenuViewModel.TriggerCurrentTourChangedAction(_sideMenuViewModel.CurrentTour);

            SubmitAction?.Invoke();
        }

        private void Cancel()
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
