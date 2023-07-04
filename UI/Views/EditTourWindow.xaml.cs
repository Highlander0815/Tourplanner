using TourplannerModel;
using System;
using System.Windows;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class EditTourWindow : Window
    {
        public EditTourWindow()
        {
            InitializeComponent();
        }

        public void Edit(TourModel currentTour, Action<TourModel> update)
        {
            var mainWindow = this.DataContext as EditTourViewModel;

            mainWindow.Name = currentTour.Name;
            mainWindow.Description = currentTour.Description;
            mainWindow.From = currentTour.From;
            mainWindow.To = currentTour.To;
            mainWindow.TransportType = currentTour.TransportType;

            mainWindow.SubmitAction += (tour) => update(tour);
            mainWindow.SubmitAction += (tour) => this.DialogResult = true;   
            mainWindow.CancelEvent += (o, e) => this.DialogResult = false;
        }
    }
}
