using TourplannerModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using BLL.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BLL
{
    public class ImportExportManager
    {
        private TourHandler _tourHandler;
        private RESTHandler _restHandler;
        public ImportExportManager(/*TourHandler tourHandler, TourLogHandler tourLogHandler*/)
        {
            _tourHandler = new TourHandler();
            _restHandler = new RESTHandler();
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
        
        public async Task ImportTour()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    TourModel tour = JsonConvert.DeserializeObject<TourModel>(json);
                    if (tour != null)
                    {
                        _tourHandler = new TourHandler();
                        _tourHandler.AddTour(tour);
                        Task<TourModel> result = _restHandler.Rest.Request(tour);
                        tour = await result;
                        _tourHandler = new TourHandler();
                        _tourHandler.UpdateTour(tour);
                    }
                }
                catch (ResponseErrorOfApiException ex)
                {
                    throw ex;
                }
                catch(Exception)
                {
                    throw new SomethingWentWrongException();
                }
               
            }
        }
    }
}        
