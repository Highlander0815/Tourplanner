using TourplannerModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using log4net;

namespace BLL
{
    public class ImportExportManager
    {
        private TourHandler _tourHandler;
        private TourLogHandler _tourLogHandler;
        public ImportExportManager(TourHandler tourHandler, TourLogHandler tourLogHandler)
        {
            _tourHandler = tourHandler;
            _tourLogHandler = tourLogHandler;
        }

        private class TempTour
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string TransportType { get; set; }
            public string TourDistance { get; set; }
            public string EstimatedTime { get; set; }
            public IEnumerable<TourLogModel> TourLogs { get; set; }
        }

        public void ExportTour(int tourid)
        {
            TourModel tour = _tourHandler.GetTour(tourid);
            IEnumerable<TourLogModel> tourlogs = _tourLogHandler.GetTourLogsById(tourid);

            TempTour exportTour = new TempTour(){
                Name = tour.Name,
                Description = tour.Description,
                From = tour.From,
                TransportType = tour.TransportType,
                TourDistance = tour.TourDistance,
                EstimatedTime = tour.EstimatedTime,
                TourLogs = tourlogs
            };

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json";
            saveFileDialog.FileName = "Tour_" + tourid.ToString() + "_" + tour.Name + ".json";

            if (saveFileDialog.ShowDialog() == true)
                JsonConvert.SerializeObject(exportTour, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
        }
    }
}
