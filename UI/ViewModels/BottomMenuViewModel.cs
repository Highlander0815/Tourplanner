﻿using BLL;
using iText.StyledXmlParser.Node;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TourplannerModel;

namespace UI.ViewModels
{
    public class BottomMenuViewModel : ViewModelBase
    {
        //Actions
        public event Action<TourLogModel> selectedTourLogChangedAction;

        private TourModel _currentTour; //has to be saved because the currentTour contains the TourLogs which should be displayed
        private SideMenuViewModel _sideMenuViewModel;
        //List of TourLogs
        private ObservableCollection<TourLogModel> _tourLogs;
        public ObservableCollection<TourLogModel> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }


        private TourLogHandler _tourLogHandler;
        public BottomMenuViewModel(SideMenuViewModel sideMenuViewModel, TourLogHandler tourLogHandler)
        {
            selectedTourLogChangedAction += HandleTourLogChange;
            _sideMenuViewModel = sideMenuViewModel;
            _sideMenuViewModel.currentTourChangedAction += HandleTourChanged;

            _tourLogs = new ObservableCollection<TourLogModel>();

            _tourLogHandler = tourLogHandler;
            //_tourLogs = new ObservableCollection<TourLogModel>(tourLogHandler.GetTourLogs());
        }

        //current TourLog = Selected item in the dataGrid. Is needed to know the currentTourLog when you want to edit/delete it.
        private TourLogModel _currentTourLog;
        public TourLogModel CurrentTourLog
        {
            get
            {
                return _currentTourLog;
            }
            set
            {
                if (_currentTourLog != value)
                {
                    _currentTourLog = value;
                    OnPropertyChanged(nameof(CurrentTourLog));
                }
            }
        }

        //Events
        public event EventHandler OpenAddTourLog;
        public event EventHandler OpenEditTourLog;

        //Commands
        private RelayCommand? _addCommand = null;
        private RelayCommand? _editCommand = null;
        private RelayCommand? _deleteCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(OpenAddTourLogW);
        public RelayCommand EditCommand => _editCommand ??= new RelayCommand(OpenEditTourLogW);
        public RelayCommand DeleteCommand => _deleteCommand ??= new RelayCommand(Delete);


        //private Methods
        private void OpenAddTourLogW()
        {
            if(_currentTour != null) //otherwise the new TourLog can not be added to a Tour
            {
                _logger.Info("Add Tourlog Window got oppened");
                OpenAddTourLog?.Invoke(this, EventArgs.Empty);
                TourLogs = new ObservableCollection<TourLogModel>(_tourLogHandler.GetTourLogsById(_currentTour.Id));
                _logger.Info($"A new Tourlog was added to the Tour with the ID: {_currentTour.Id}");
            }
            else
            {
                ShowMessageBox("No tour selected!");
            }                
        }

        private void OpenEditTourLogW()
        {
            if(_currentTourLog != null) //if no tourLog is selected their is nothing which can be edited
            {
                _logger.Info("Edit Tourlog Window got oppened");
                OpenEditTourLog?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ShowMessageBox("No log selected!");
            }
            
        }

        private void Delete() 
        {
            if (_tourLogs != null && _tourLogs.Contains(_currentTourLog))
            {
                _tourLogHandler.DeleteTourLog(_currentTourLog.Id);
                TourLogs.Remove(_currentTourLog);
                _logger.Info($"A Tourlog got deleted");
                CurrentTourLog = null;
                _sideMenuViewModel.TriggerCurrentTourChangedAction(_currentTour);
            }
            else
            {
                ShowMessageBox("No log selected!");
            }
        }

        public void UpdateList()
        {
            TourLogs = new ObservableCollection<TourLogModel>(_tourLogHandler.GetTourLogsById(_currentTour.Id));
            _logger.Info($"A TourLog of the Tour with the ID: {_currentTour.Id} got modified");
        }

        private void HandleTourLogChange(TourLogModel tourLog)
        {
            CurrentTourLog = tourLog; //When the CurrentTourLog changes the CurrentTourLog has to be updated
        }

        private void HandleTourChanged(TourModel tour) //when a Tour gets selected in the SideMenu the TourLogs which are displayed in the BottomMenu have to be displayed.
        {
            if (tour != null)
            {
                _currentTour = tour;
                TourLogs = new ObservableCollection<TourLogModel>(_tourLogHandler.GetTourLogsById(_currentTour.Id));
            }
            else
            {
                _currentTour = null;
                TourLogs = null;
            }
            CurrentTourLog = null;
        }
    }
}
