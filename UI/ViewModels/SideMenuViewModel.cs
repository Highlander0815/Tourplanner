using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.ViewModels
{
    public class SideMenuViewModel
    {
        public ICommand AddTour { get; }
        public ICommand ModifyTour { get; }
        public ICommand DeleteTour { get; }
    }
}
