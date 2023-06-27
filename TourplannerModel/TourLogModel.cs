using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourplannerModel
{
    [Table("TourLogs")]
    public class TourLogModel
    {
        [Key]
        public int Id { get; private set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int Difficulty { get; set; }
        public int Total_time { get; set; }
        public int Rating { get; set; }
    }
}
