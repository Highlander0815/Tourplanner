using System.Windows;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for AddTourLogWindow.xaml
    /// </summary>
    public partial class AddTourLogWindow : Window
    {
        public AddTourLogWindow()
        {
            InitializeComponent();
        }
        public void Init(/*Action<TourLogModel> save*/)
        {
            var mainWindow = DataContext as AddTourLogViewModel;
            mainWindow.SetValues();
            mainWindow.AddEvent += () => this.DialogResult = true;
            mainWindow.CancelEvent += (o, e) => this.DialogResult = false;
        }
    }
}
