using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using BLL.Models;
using System.Net.NetworkInformation;
using System.Collections;
using System;
using System.Drawing;
using System.IO;
using static System.Drawing.Image;
using MiNET.Sounds;


namespace BLL
{
    public class Rest
    {
        private static HttpClient _client = new HttpClient();
        private Tour _tour;
        public Rest(Tour tour) 
        {
            _tour = tour;   
        }   

        public async void Request()
        {
            {
                string key = "vGz1EP3woj6YXCYOmGDSoh9RFcmWnzdq"; //configureation file 
                string routeImageURL = $"https://www.mapquestapi.com/staticmap/v5/map?start={Uri.EscapeDataString(_tour.From)}&end={Uri.EscapeDataString(_tour.To)}&size=600,400&key={key}";
                //string routeImageURL = $"https://www.mapquestapi.com/directions/v2/route?key={key}&from={Uri.EscapeDataString(_tour.From)}&to={Uri.EscapeDataString(_tour.To)}&size=600,400";

                HttpResponseMessage response = await _client.GetAsync(routeImageURL);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                   

                     Image image = ByteArrayToImage(imageBytes);

                    // Prozessieren Sie die Bildbytes nach Bedarf (z.B. speichern Sie sie in einer Datei oder zeigen Sie sie in einem Image-Steuerelement an)
                }
                else
                {
                    // Behandeln Sie den API-Anfragefehler
                }
            }
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                var image = Image.FromStream(memoryStream);
                image.Save("C:\\Users\\wiede\\Documents\\Fachhochschule\\4.Semester\\SWEN\\Projekt\\Tourplanner\\Tourplanner\\BLL\\test1.png", System.Drawing.Imaging.ImageFormat.Png);
                return image;
            }
        }


    }

}
