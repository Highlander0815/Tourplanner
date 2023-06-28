using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL;
using BLL.Models;
using Microsoft.VisualBasic;
using MiNET.Blocks;
using UI.Views;

namespace UI.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        //Actions
        public Action<Tour> currentTourChangedAction;

        //Events
        public event EventHandler OpenAddTour;
        public event EventHandler OpenEditTour;

        //Commands
        private RelayCommand? _addCommand = null;
        private RelayCommand? _editCommand = null;
        private RelayCommand? _deleteCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(OpenAddTourW);
        public RelayCommand EditCommand => _editCommand ??= new RelayCommand(OpenEditTourW);
        public RelayCommand DeleteCommand => _deleteCommand ??= new RelayCommand(Delete);

        //Attributes
        private  ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set 
            { 
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        
        //Tour
        private Tour _currentTour;
        public Tour CurrentTour {
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

        //Constructor
        public SideMenuViewModel()
        {
            _tours = new ObservableCollection<Tour>();

            _tours.Add(new Tour("test", "test", "test", "test", "test"));
            _tours.Add(new Tour("test1", "test1", "test1", "test1", "test1"));
            _tours.Add(new Tour("MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour"));
        }

        //private Methods
        private void OpenAddTourW()
        {
            this.OpenAddTour?.Invoke(this, EventArgs.Empty);
        }
        private void Add(Tour tour)
        {
            _tours.Add(tour);
        }

        private void OpenEditTourW()
        {
            this.OpenEditTour?.Invoke(this, EventArgs.Empty);
        }

        private void Delete()
        {
            if (_tours.Contains(_currentTour))
            {
                _tours.Remove(_currentTour);
            }
            else
            {
                //noch implementieren
            }
        }

        //public Methods
        public void Save(Tour t)
        {
            Add(t);
        }
        public void UpdateList(Tour tour)
        {
            if (_tours.Contains(_currentTour))
            {
                _tours[_tours.IndexOf(_currentTour)] = tour;
            }
            else
            {
                //noch ka was dann passiert
            }
        }

    }
}
