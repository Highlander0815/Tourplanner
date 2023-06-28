using BLL.Models;
using MiNET.UI;
using MiNET.Utils.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public void Edit(Tour currentTour, Action<Tour> update)
        {
            var mainWindow = this.DataContext as EditTourViewModel;

            mainWindow.Tour = currentTour;
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
