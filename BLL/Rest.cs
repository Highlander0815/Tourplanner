using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TourplannerModel;
using System.Net.NetworkInformation;
using System.Collections;
using System;
using System.Drawing;
using System.IO;
using static System.Drawing.Image;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using MiNET.Utils;

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
            _client = new HttpClient();
            string transportType = "";
            if(tour.TransportType == "Car") //the rest of the selection possibilities are named like on the website so it has not to be changed before requesting
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
            HttpResponseMessage response2 = await _client.GetAsync(routeDataURL);
            if (response2.IsSuccessStatusCode)
            {
                string responseBody = await response2.Content.ReadAsStringAsync();
                dynamic jsonData  = JsonConvert.DeserializeObject(responseBody);

                tour.EstimatedTime = jsonData["route"]["formattedTime"];
                tour.TourDistance = jsonData["route"]["distance"];
            }
            else
            {
                // Behandeln Sie den API-Anfragefehler
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
