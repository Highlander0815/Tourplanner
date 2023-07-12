using TourplannerModel;
using static System.Drawing.Image;
using Newtonsoft.Json;
using BLL.Exceptions;
using System.Configuration;

namespace BLL
{
    public class Rest
    {
        private HttpClient _client;
        public Rest() 
        {
        }   

        public async Task<TourModel> Request(TourModel tour)
        {
            try
            {
                _client = new HttpClient();
                string transportType = "";
                if (tour.TransportType == "Car") //the rest of the selection possibilities are named like on the website so it has not to be changed before requesting
                {
                    transportType = "fastest";
                }
                else
                {
                    transportType = tour.TransportType;
                }
                string key = "vGz1EP3woj6YXCYOmGDSoh9RFcmWnzdq"; //configureation file 
                string routeImageURL = $"https://www.mapquestapi.com/staticmap/v5/map?start={Uri.EscapeDataString(tour.From)}&end={Uri.EscapeDataString(tour.To)}&size=600,400&key={key}";
                string routeDataURL = $"https://www.mapquestapi.com/directions/v2/route?key={key}&from={Uri.EscapeDataString(tour.From)}&to={Uri.EscapeDataString(tour.To)}&routeType={transportType}&uni=k";

                HttpResponseMessage response2 = await _client.GetAsync(routeDataURL);
                if (response2.IsSuccessStatusCode)
                {
                    string responseBody = await response2.Content.ReadAsStringAsync();
                    dynamic jsonData = JsonConvert.DeserializeObject(responseBody);

                    if (jsonData["info"]["statuscode"] != "0") //kontrolliere mithilfe von postman wann etwas anderes als 200 zurück kommt
                    {
                        string errorString = jsonData["info"]["messages"][0].ToString();
                        throw new ResponseErrorOfApiException(errorString);
                    }

                    tour.EstimatedTime = jsonData["route"]["formattedTime"];
                    tour.TourDistance = jsonData["route"]["distance"];
                }
                else
                {
                    // Behandeln Sie den API-Anfragefehler
                }

                HttpResponseMessage response = await _client.GetAsync(routeImageURL);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();


                    tour.Image = ByteArrayToImage(imageBytes, tour);
                }
                else
                {
                    // Behandeln Sie den API-Anfragefehler
                }
            }
            catch (ResponseErrorOfApiException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
           
            return tour;
        }

        public string ByteArrayToImage(byte[] byteArray, TourModel tour)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {                
                var image = FromStream(memoryStream);
                string name = tour.Id.ToString();

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), name);
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                
                return filePath;
            }            
        }
    }
}
