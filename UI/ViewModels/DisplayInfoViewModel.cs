using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Views;

namespace UI.ViewModels
{
    public class DisplayInfoViewModel : ViewModelBase
    {
        public Tour currentTour;
       // private Action _test;
        public DisplayInfoViewModel(/*CenterWindowViewModel centerViewModel*/) 
        {
            //_centerViewModel = centerViewModel;
            //_currentTour = _centerViewModel.currentTour;
                //_sideMenu = sideMenu;
                /*_test += centerViewModel.currentTourChangedAction = () =>
                {
                    _currentTour = sideMenu.CurrentTour;
                    //CurrentTourChanged();
                    SetValues();
                };*/
        }
        CenterWindowViewModel _centerViewModel;
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

        private string _tourDistance;
        public string TourDistance
        {
            get { return _tourDistance; }
            set
            {
                _tourDistance = value;
                OnPropertyChanged(nameof(TourDistance));
            }
        }
        private string _estimatedTime;
        public string EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                _estimatedTime = value;
                OnPropertyChanged(nameof(EstimatedTime));
            }
        }
        /*private void SetValues()
        {
            Name = _currentTour.Name;
            Description = _currentTour.Description;
            From = _currentTour.From;
            To = _currentTour.To;
            TransportType = _currentTour.TransportType;
            TourDistance = _currentTour.TourDistance;
            EstimatedTime = _currentTour.EstimatedTime;
        }*/

        public void GetInfoView()
        {
            if(currentTour!= null)
            {
                Name = currentTour.Name;
                Description = currentTour.Description;
                From = currentTour.From;
                To = currentTour.To;
                TransportType = currentTour.TransportType;
                TourDistance = currentTour.TourDistance;
                EstimatedTime = currentTour.EstimatedTime;
            }
            
            // Führe hier die Logik aus, um die Info-Ansicht zu erstellen
            //return new DisplayInfoWindow();
        }
    }
}
