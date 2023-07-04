﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourplannerModel;
using System.Collections.ObjectModel;
using BLL;
using System.Windows.Controls;

namespace UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        public event Action<TourModel> AddEvent; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private RESTHandler _restHandler;
        private TourHandler _tourHandler;
        private Validator _validator;

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

        public AddTourViewModel(TourHandler tourHandler)
        {
            _tourHandler = tourHandler;
        } 

        private async void AddTour()
        {
            TourModel tour = new TourModel(Name, Description, From, To, TransportType);
            _tourHandler.AddTour(tour); //damit die Id gesetzt wird
            _restHandler = new RESTHandler();
            Task<TourModel> result = _restHandler.Rest.Request(tour);
            tour = await result;
            _tourHandler.UpdateTour(tour);       //damit link upgedatet wird ABER ich glaube das man das gar nicht braucht weil link ja sowieso ident bleibt     
            this.AddEvent?.Invoke(tour);
        }

        private void Cancel()
        {
            this.CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
