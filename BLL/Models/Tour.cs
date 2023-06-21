using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public string? TourDistance { get; set; } //get by Rest request //nullable gehört noch entfernt
        public string? EstimatedTime { get; set; } //get by Rest request //nullable gehört noch entfernt
        public string? Image { get; set; } //get by Rest request //nullable gehört noch entfernt


        public Tour(string name, string description, string from, string to, string transportType)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
        }

        
    }
}
