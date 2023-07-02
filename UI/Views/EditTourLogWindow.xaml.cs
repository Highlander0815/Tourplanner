﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourplannerModel;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaktionslogik für EditTourLogWindow.xaml
    /// </summary>
    public partial class EditTourLogWindow : Window
    {
        public EditTourLogWindow()
        {
            InitializeComponent();
        }
        public void Edit(TourLogModel currentTourLog, Action<TourLogModel> update)
        {
            var mainWindow = this.DataContext as EditTourLogViewModel;

            DateTime date = new DateTime(currentTourLog.Date.Year, currentTourLog.Date.Month, currentTourLog.Date.Day);
            mainWindow.Date = date;
            mainWindow.Time = currentTourLog.Time;
            mainWindow.Difficulty = currentTourLog.Difficulty;
            mainWindow.TotalTime = currentTourLog.TotalTime;
            mainWindow.Rating = currentTourLog.Rating;


            mainWindow.SubmitAction += (tour) => update(tour);
            mainWindow.SubmitAction += (tour) => this.DialogResult = true;
            mainWindow.CancelEvent += (o, e) => this.DialogResult = false;
        }
    }
}