using Microsoft.VisualBasic;

namespace TourplannerModel
{
    public class TourModel
    {
        public int Id { get; private set; }
        public ICollection<TourLogModel> TourLogs { get; set; } = null!;
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
