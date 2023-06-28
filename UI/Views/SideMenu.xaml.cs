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
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SideMenuViewModel dataContext = (SideMenuViewModel)DataContext;
            dataContext.OpenAddTour += (sender, ev) =>
            {
                AddTourWindow addTourW = new AddTourWindow();
                addTourW.Init(dataContext.Save);
                addTourW.ShowDialog(); 
            };

            dataContext.OpenEditTour += (sender, ev) =>
            {
                EditTourWindow editTourW = new EditTourWindow();
                editTourW.Edit(dataContext.CurrentTour, dataContext.UpdateList);
                editTourW.ShowDialog();
            };
        }
    }
}
