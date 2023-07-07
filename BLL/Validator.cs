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
            if (tourToValidate != null )
            {
                return !(    
                         string.IsNullOrEmpty(tourToValidate.Name) ||
                         (tourToValidate.TransportType != "car" && tourToValidate.TransportType != "bicycle" && tourToValidate.TransportType != "pedestrian") ||
                         string.IsNullOrEmpty(tourToValidate.Description) ||
                         string.IsNullOrEmpty(tourToValidate.From) ||
                         string.IsNullOrEmpty(tourToValidate.To)
                       ); 
            }
            return false;
        }

        /*public bool LogValidation(TourLogModel logToValidate)
        {
            if (logToValidate != null)
            {
                if (logToValidate.DateTime != DateTime.Now && !logToValidate.IsDefined)
                {
                    return true;
                }
                return false;
            }
            return false;
        }*/
    }
}
