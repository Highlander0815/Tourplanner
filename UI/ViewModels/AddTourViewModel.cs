using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourplannerModel;
using System.Collections.ObjectModel;
using BLL;
using System.Windows.Controls;
using System.Windows;
using BLL.Logging;
using BLL.Exceptions;

namespace UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        public event Action<TourModel> AddEvent; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private RESTHandler _restHandler;
        private TourHandler _tourHandler;
        private Validator _validator;
        private TourModel _newTour;

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(AddTour);
        
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

        public AddTourViewModel(TourHandler tourHandler)
        {
            _tourHandler = tourHandler;
            IsButtonEnabled = false;
            TransportType = "Car";
        } 

        private void UpdateButtonState()
        {
            _newTour = new TourModel(_name, _description, _from, _to, _transportType);
            _validator = new Validator();
            bool allFieldsFilled = _validator.TourValidation(_newTour);

            IsButtonEnabled = allFieldsFilled;
        }
        private async void AddTour()
        {
            IsButtonEnabled = false;
            TourModel tour = new TourModel(Name, Description, From, To, TransportType);
            try
            {
                _tourHandler.AddTour(tour); //damit die Id gesetzt wird
                _restHandler = new RESTHandler();
                Task<TourModel> result = _restHandler.Rest.Request(tour);
                tour = await result;
                _tourHandler.UpdateTour(tour);       //damit link upgedatet wird ABER ich glaube das man das gar nicht braucht weil link ja sowieso ident bleibt     
                AddEvent?.Invoke(tour);
            }
            catch (ResponseErrorOfApiException responseException)
            {
                IsButtonEnabled = true;
                ShowMessageBox($"{responseException.Message} Please check your input in the to and from field");
                _tourHandler.DeleteTour(tour.Id);
                _logger.Error($"The Imput the usere entered in 'To' and/or 'From' could not be handeled from the API. The user entered:  {tour.To} & {tour.From}");
            }
            catch(Exception ex)
            {
                _tourHandler.DeleteTour(tour.Id);
                ShowMessageBox($"A new Error happened which should be handeled. The error was printed into the Log File");
                _logger.Error($"New Exception happened: {ex}");
            }
           
        }

        private void Cancel()
        {
            CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
