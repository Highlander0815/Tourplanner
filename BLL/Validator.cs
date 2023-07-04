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
        public Validator(TourModel tourToValidate) 
        {  
        }

        public bool TourValidation(TourModel tourToValidate)
        {
            if (tourToValidate != null )
            {
                if (tourToValidate.Name != null && tourToValidate.Name != "" &&
                   tourToValidate.TransportType == "car" || tourToValidate.TransportType == "bicycle" || tourToValidate.TransportType == "pedestrian" &&
                   tourToValidate.Description != null && tourToValidate.Description != "" &&
                   tourToValidate.From != null && tourToValidate.From != "" &&
                   tourToValidate.To != null && tourToValidate.To != "")
                {
                    return true;
                }
                return false;
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
