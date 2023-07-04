using System;
using System.Threading.Tasks;
using TourplannerModel;
using BLL;



namespace UI.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {
        public event Action<TourModel> currentTourChangedAction;
        public event Action<TourModel> SubmitAction; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private RESTHandler _restHandler;
        private TourHandler _tourHandler;

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(EditTour);
        
        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        private TourModel _tour;
        private TourModel _currentTour;
        public TourModel Tour
        {  get { return _tour; } 
           set { _tour = value; }
        }

        public TourModel CurrentTour
        {
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }
        private string _to;
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        private string _transportType;
        public string TransportType
        {
            get { return _transportType; }
            set
            {
                _transportType = value;
                OnPropertyChanged(nameof(TransportType));
            }
        }
        public EditTourViewModel(TourHandler tourHandler)
        {
            _restHandler = new RESTHandler();
            _tourHandler = tourHandler;
        }

        private async void EditTour()
        {
            _tour.Name = _name;
            _tour.Description = _description;
            _tour.From = _from;                
            _tour.To = _to;
            _tour.Description = _description;
            Task<TourModel> result = _restHandler.Rest.Request(_tour);
            _tour = await result;
            _tourHandler.UpdateTour(_tour);
            SubmitAction?.Invoke(_tour);
        }

        private void Cancel()
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
