using Org.BouncyCastle.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public int? Popularity { get; set; }
        [NotMapped]
        public int? ChildFriendliness { get; set; }

        [NotMapped]
        public bool Visible { get; set; } = true;

        public string Searchstring { get => CreateSearchString(); }
        public ObservableCollection<TourLogModel> TourLogs { get; set; } = new ObservableCollection<TourLogModel>();
        public TourModel(string name, string description, string from, string to, string transportType)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            TourLogs = new ObservableCollection<TourLogModel>();
            Visible = true;
        }
        public TourModel() { }

        private string CreateSearchString()
        {
            string searchString = string.Concat(Name, Description, From, To, EstimatedTime, Popularity.ToString(), TransportType, ChildFriendliness.ToString());
            if(TourLogs != null)
            {
                foreach (var tourLog in TourLogs)
                {
                    searchString += string.Concat(tourLog.DateTime.ToString(), tourLog.Comment, (tourLog.Rating).ToString(), Enum.GetName(tourLog.Difficulty), tourLog.TotalTime);
                }
            }
            
            return searchString;   
        }
    }
}
