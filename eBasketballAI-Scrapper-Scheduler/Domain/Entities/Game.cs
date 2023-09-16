using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string PlayerA { get; set; } = string.Empty;

        public string TeamA { get; set; } = string.Empty;
        public string TeamB { get; set; } = string.Empty;
        public string PlayerB { get; set; } = string.Empty;
        public DateTime MatchDate { get; set; } = DateTime.Now;
        public int ScoreA { get; set; } = 0;
        public int ScoreB { get; set; } = 0;
        public string Url { get; set; } = string.Empty;
    }
}
