using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourplannerModel
{
    [Table("TourLogs")]
    public class TourLogModel
    {
        [Key]
        public int Id { get; private set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

        public TourLogModel(DifficultyEnum difficulty, TimeSpan totalTime, int rating) 
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }

        public TourLogModel()
        {
            
        }
    }
}
