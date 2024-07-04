
namespace RunTracker.API.Data{

    public partial class RunActivity
    {
        public int RunId { get; set; }

        public int? UserId { get; set; }

        public string Location { get; set; } = null!;

        public DateTime DateTimeStarted { get; set; }

        public DateTime DateTimeEnded { get; set; }

        public decimal? Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan AveragePace { get; set; }

        public virtual User? User { get; set; }
    }
}