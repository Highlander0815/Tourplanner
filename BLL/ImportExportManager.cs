using TourplannerModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using log4net;
using MiNET.Utils;

namespace BLL
{
    public class ImportExportManager
    {
        private TourHandler _tourHandler;
        public ImportExportManager(TourHandler tourHandler)
        {
            _tourHandler = tourHandler;
        }

        public void ExportTour(int tourid)
        {
            TourModel tour = _tourHandler.GetTour(tourid);
            
            string json = JsonConvert.SerializeObject(tour, Formatting.Indented);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json";
            saveFileDialog.FileName = "Tour_" + tourid.ToString() + "_" + tour.Name + ".json";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
        
        public void ImportTour()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                TourModel tour = JsonConvert.DeserializeObject<TourModel>(json);
                if (tour != null)
                    _tourHandler.AddTour(tour);
            }
        }
    }
}        
