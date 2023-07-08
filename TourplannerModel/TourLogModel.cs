using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TourplannerModel
{
    [Table("TourLogs")]
    public class TourLogModel
    {
        [Key]
        public int Id { get; private set; } 
        public DateTime DateTime { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }
        [JsonIgnore]
        public TourModel TourModel { get; set; } = null!;

        public TourLogModel(DateTime dateTime, DifficultyEnum difficulty, TimeSpan totalTime, int rating) 
        {
            DateTime = dateTime;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }
        public TourLogModel(DateTime dateTime, DifficultyEnum difficulty, TimeSpan totalTime, int rating, TourModel tour)
        {
            DateTime = dateTime;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
            TourModel = tour;
        }
        public TourLogModel() { }
    }
}
