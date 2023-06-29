using MiNET.Worlds;
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
    /// Interaction logic for BottomMenu.xaml
    /// </summary>
    public partial class BottomMenu : UserControl
    {
        public BottomMenu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BottomMenuViewModel dataContext = (BottomMenuViewModel)DataContext;
            dataContext.OpenAddTourLog += (sender, ev) =>
            {
                AddTourLogWindow addTourLogW = new AddTourLogWindow();
                addTourLogW.Init(dataContext.Save);
                addTourLogW.ShowDialog();
            };

            dataContext.OpenEditTourLog += (sender, ev) =>
            {
                EditTourLogWindow editTourLogW = new EditTourLogWindow();
                editTourLogW.Edit(dataContext.CurrentTourLog, dataContext.UpdateList);
                editTourLogW.ShowDialog();
            };
        }
    }
}
