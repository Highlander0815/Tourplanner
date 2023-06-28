using TourplannerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourplannerModel;
using UI.Views;

namespace UI.ViewModels
{
    public class DisplayRouteViewModel : ViewModelBase
    {
        public DisplayRouteViewModel()
        {

        }

        public TourModel currentTour;
        private BitmapImage _currentTourImage;
        public BitmapImage CurrentTourImage
        {
            get { return _currentTourImage; }
            set
            {
                _currentTourImage = value;
                OnPropertyChanged(nameof(CurrentTourImage));
            }
        }

        public void GetRouteView()
        {
            if (currentTour != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(currentTour.Image);
                image.EndInit();
                CurrentTourImage = image;
            }

        }
    }
}
