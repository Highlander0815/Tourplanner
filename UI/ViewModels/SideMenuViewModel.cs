﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL;
using BLL.Models;
using Microsoft.VisualBasic;
using UI.Views;

namespace UI.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        public event EventHandler OpenAddTour;
        private readonly ObservableCollection<Tour> _tours;

        public IEnumerable<Tour>  Tours => _tours;



        //Commands
        /*private RelayCommand _addCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(AddTour);*/
        private RelayCommand _addCommand = null;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(OpenAddTourW);
        public ICommand ModifyTour { get; }
        public ICommand DeleteTour { get; }


        public SideMenuViewModel()
        {
            _tours = new ObservableCollection<Tour>();

            _tours.Add(new Tour("test", "test", "test", "test", "test"));
            _tours.Add(new Tour("test1", "test1", "test1", "test1", "test1"));
            _tours.Add(new Tour("MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour", "MoreAdvancedTour"));
        }
        public void OpenAddTourW()
        {
            this.OpenAddTour?.Invoke(this, EventArgs.Empty);
        }
        public void Add(Tour tour)
        {
            _tours.Add(tour);
        }
        public void Speichern(Tour t) 
        {
            Add(t);
        }

        /*private void AddTour()
        {
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.ShowDialog();
        }*/
    }
}
