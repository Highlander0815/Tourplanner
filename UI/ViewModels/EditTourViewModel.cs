using System;
using System.Threading.Tasks;
using TourplannerModel;
using BLL;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Validator = BLL.Validator;

namespace UI.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {
        private SideMenuViewModel _sideMenuViewModel;
        public event Action<TourModel> SubmitAction; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private RESTHandler _restHandler;
        private TourHandler _tourHandler;
        private Validator _validator;
        private TourModel _newTour;

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(EditTour);
        
        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);

        public ObservableCollection<string> TransportTypes { get; set; } = new ObservableCollection<string>()
        {
            "Car",
            "Bicycle",
            "Pedestrian"
        };

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

        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value; 
                OnPropertyChanged(nameof(Name));
                UpdateButtonState();
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
                UpdateButtonState();
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
                UpdateButtonState();
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
                UpdateButtonState();
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
                UpdateButtonState();
            }
        }
        public EditTourViewModel(SideMenuViewModel sideMenuViewModel, TourHandler tourHandler)
        {
            _sideMenuViewModel = sideMenuViewModel;
            _tourHandler = tourHandler;
        }

        private void UpdateButtonState()
        {
            _newTour = new TourModel(_name, _description, _from, _to, _transportType);
            _validator = new Validator();
            bool allFieldsFilled = _validator.TourValidation(_newTour);

            IsButtonEnabled = allFieldsFilled;
        }
        private async void EditTour()
        {
            /*TourModel tour = new TourModel(Name, Description, From, To, TransportType);
            _restHandler = new RESTHandler();
            Task<TourModel> result = _restHandler.Rest.Request(tour);
            tour = await result;
            _tour = tour;*/
            IsButtonEnabled = false;
            TourModel currentTour = _sideMenuViewModel.CurrentTour; //Zeile 101 bis 108 ist neuer code und muss eventuell wieder gelöscht werden
            currentTour.Name = _name;
            currentTour.Description = _description;
            currentTour.From = _from;
            currentTour.To = _to;
            currentTour.TransportType = _transportType;
            _restHandler = new RESTHandler();
            Task<TourModel> result = _restHandler.Rest.Request(currentTour);
            currentTour = await result;
            _tourHandler.UpdateTour(currentTour);
            this.SubmitAction?.Invoke(currentTour);
        }

        private void Cancel()
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
