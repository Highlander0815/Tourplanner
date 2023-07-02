using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourplannerModel
{
    [Table("TourLogs")]
    public class TourLogModel
    {
        [Key]
        public int Id { get; private set; } 
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }
        public TourModel TourModel { get; set; } = null!;

        public TourLogModel(DateOnly date, TimeOnly time, DifficultyEnum difficulty, TimeSpan totalTime, int rating) 
        {
            Date = date;
            Time = time;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }
        public TourLogModel(DateOnly date, TimeOnly time, DifficultyEnum difficulty, TimeSpan totalTime, int rating, TourModel tour)
        {
            Date = date;
            Time = time;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
            TourModel = tour;
        }
        public TourLogModel() { }
    }
}
