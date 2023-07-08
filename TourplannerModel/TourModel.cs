using System.Collections.ObjectModel;

namespace TourplannerModel
{
    public class TourModel
    {
        public int Id { get; private set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string TransportType { get; set; } = null!;
        public float? TourDistance { get; set; }
        public string? EstimatedTime { get; set; }
        public string? Image { get; set; }
        public int? Popularity { get; set; }
        public int? ChildFriendliness { get; set; }
        public ObservableCollection<TourLogModel> TourLogs { get; private set; } = null!;
        public TourModel(string name, string description, string from, string to, string transportType)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            TourLogs = new ObservableCollection<TourLogModel>();
        }
        public TourModel() { }
    }
}
