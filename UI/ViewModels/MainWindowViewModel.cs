using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class MainWindowViewModel
    {
        private DisplayRouteViewModel _displayRouteViewModel;
        private DisplayInfoViewModel _displayInfoViewModel;
        public MainWindowViewModel(DisplayRouteViewModel displayRouteViewModel, DisplayInfoViewModel displayInfoViewModel) //hier noch die anderen Adden
        {
            _displayRouteViewModel = displayRouteViewModel;
            _displayInfoViewModel = displayInfoViewModel;
           
        }
    }
}
