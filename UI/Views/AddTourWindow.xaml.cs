using System;
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
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class AddTourWindow : Window
    {
        public AddTourWindow()
        {
            InitializeComponent();
        }

        public void Init(Action<TourModel> save)
        {
            var mainWindow = DataContext as AddTourViewModel;
            mainWindow.AddEvent += (o) => save(o);
            mainWindow.AddEvent += (o) => DialogResult = true;   
            mainWindow.CancelEvent += (o, e) => DialogResult = false;
        }

        /*private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }*/
    }
}
