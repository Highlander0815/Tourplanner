using TourplannerModel;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using log4net;

namespace UI.ViewModels
{
    public class DisplayRouteViewModel : ViewModelBase
    {
        public DisplayRouteViewModel()
        {

        }
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SideMenuViewModel));
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
                        _logger.Info("Tour Image got updated");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"This Tour {currentTour.Id} does not contain an Image", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _logger.Error($"This Tour {currentTour.Id} does not contain an Image");
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
