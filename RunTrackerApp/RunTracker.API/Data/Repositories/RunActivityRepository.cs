
namespace RunTracker.API.Data.Repositories{

    public class RunActivityRepository : IRepository<RunActivity>
    {
        private readonly RunTrackerDbContext _context;
        private readonly ILogger<RunActivityRepository> _logger;

        public RunActivityRepository(RunTrackerDbContext context, ILogger<RunActivityRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public RunActivity GetById(int id)
        {
            return _context.RunActivities.Find(id);
        }

        public IEnumerable<RunActivity> GetAll()
        {
            return _context.RunActivities.ToList();
        }

        public void Add(RunActivity activity)
        {
            _context.RunActivities.Add(activity);
            _context.SaveChanges();
        }

        public void Update(RunActivity activity)
        {
            _context.RunActivities.Update(activity);
            _context.SaveChanges();
        }

        public void Delete(RunActivity activity)
        {
            _context.RunActivities.Remove(activity);
            _context.SaveChanges();
        }
    }
}