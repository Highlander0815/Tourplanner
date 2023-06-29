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

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new TourLogModel(DifficultyEnum.Beginner, TimeSpan.Zero, 1);
        }
    }
}
