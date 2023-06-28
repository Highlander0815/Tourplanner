﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using TourplannerModel;
using UI.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using Tourplanner;
using System.ComponentModel;
using BLL;



namespace UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        public event Action<TourModel> AddEvent; //event which will be fired by the SubmitButton
        public event EventHandler CancelEvent; //event which will be fired by the CancelButton
        private TourManager _tourManager; 

        //Commands
        private RelayCommand _submitCommand = null;
        public RelayCommand SubmitCommand => _submitCommand ??= new RelayCommand(AddTour);
        
        private RelayCommand _cancelCommand = null;
        public RelayCommand CancelCommand => _cancelCommand ??= new RelayCommand(Cancel);


        private readonly TourModel _tour;
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

        public AddTourViewModel()
        {
        } 

        private async void AddTour()
        {
            TourModel tour = new TourModel(Name, Description, From, To, TransportType);
            _tourManager = new TourManager(tour);
            Task<TourModel> result = _tourManager.Rest.Request(tour);
            tour = await result;
            this.AddEvent?.Invoke(tour);
        }

        private void Cancel()
        {
            this.CancelEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
