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
                return !(string.IsNullOrEmpty(tourToValidate.Name) ||
                        !((tourToValidate.TransportType != "car") && (tourToValidate.TransportType != "bicycle") && (tourToValidate.TransportType != "pedestrian")) ||
                        string.IsNullOrEmpty(tourToValidate.Description) ||
                        string.IsNullOrEmpty(tourToValidate.From) ||
                        string.IsNullOrEmpty(tourToValidate.To));
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
