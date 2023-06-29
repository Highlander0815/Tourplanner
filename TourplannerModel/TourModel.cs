using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace TourplannerModel
{
    public class TourModel
    {
        public TourModel(string name, string description, string from, string to, string transportType)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
        }
        public TourModel() 
        { 
        
        }
        public int Id { get; private set; }
        public ObservableCollection<TourLogModel> TourLogs { get; set; } = new ObservableCollection<TourLogModel>();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string TransportType { get; set; } = null!;
        public string? TourDistance { get; set; }
        public string? EstimatedTime { get; set; }
        public string? Image { get; set; }
    }
}
