using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;

namespace BLL
{
    public class Validator
    {
        public Validator()
        {
        }

        public bool TourValidation(TourModel tourToValidate)
        {
            if (tourToValidate != null)
            {
                bool name = !string.IsNullOrEmpty(tourToValidate.Name); 
                bool description = !string.IsNullOrEmpty(tourToValidate.Description);
                bool from = !string.IsNullOrEmpty(tourToValidate.From);
                bool to = !string.IsNullOrEmpty(tourToValidate.To);
                bool transportType = ((tourToValidate.TransportType == "Car") || (tourToValidate.TransportType == "Bicycle") || (tourToValidate.TransportType == "Pedestrian"));
                return (name && description && from && transportType && to);
            }
            return false;
        }

        public bool TourLogValidation(TourLogModel logToValidate)
        {
            if (logToValidate != null)
            {
                return (logToValidate.DateTime != null) && (logToValidate.TotalTime > TimeSpan.Zero) && Enum.IsDefined(typeof(DifficultyEnum), logToValidate.Difficulty) && (logToValidate.Rating >= 1) && (logToValidate.Rating <= 5) && !string.IsNullOrEmpty(logToValidate.Comment);
            }
            return false;
        }   
        
    }
}
