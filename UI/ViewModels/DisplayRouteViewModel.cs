using TourplannerModel;
using System;
using System.Windows.Media.Imaging;
using System.IO;

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

        public void UpdateRouteView()
        {
            if (currentTour != null)
            {
                if (currentTour.Image != null)
                {
                    /*BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(currentTour.Image);
                    image.EndInit();
                    CurrentTourImage = image;*/



                    try
                    {
                        using (var imageStream = new FileStream(currentTour.Image, FileMode.Open, FileAccess.Read))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = imageStream;
                            bitmapImage.EndInit();

                            CurrentTourImage = bitmapImage;
                        }
                    }
                    catch
                    {
                        throw new Exception("Image not found");
                    }
                    
                }
            }
            else
            {
                CurrentTourImage = null;
            }

        }
    }
}
