using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public class Tour
    {
        private string Name { get; set; }
        private string Description { get; set; }
        private string From { get; set; }
        private string To { get; set; }
        private string TransportType { get; set; }
        private string TourDistance { get; set; } //get by Rest request
        private string EstimatedTime { get; set; } //get by Rest request
        private string Image { get; set; } //get by Rest request
    }
}
