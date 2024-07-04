
namespace RunTracker.API.Data{

    public partial class User
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public DateTime BirthDate { get; set; }

        public int? Age { get; set; }

        public decimal? Bmi { get; set; }

        public virtual ICollection<RunActivity> RunActivities { get; set; } = new List<RunActivity>();
    }
}