
using RunTracker.API.Data;
using RunTracker.API.Data.Repositories;

namespace RunTracker.API.Services{

    public class RunActivityService : IRunActivityService
    {
        private readonly IRepository<RunActivity> _actRepository;
        private readonly ILogger<RunActivityService> _logger;

        public RunActivityService(IRepository<RunActivity> actRepository, ILogger<RunActivityService> logger)
        {
            _actRepository = actRepository;
            _logger = logger;
        }

        public void AddActivity(RunActivity activity)
        {
             // Calculate the Duration and AveragePace
            activity.Duration = activity.DateTimeEnded - activity.DateTimeStarted;

            double paceSecondsPerKm = activity.Duration.TotalSeconds / (double)activity.Distance;
            activity.AveragePace = TimeSpan.FromSeconds(paceSecondsPerKm);

            _actRepository.Add(activity);
            
        }

        public void DeleteActivity(int id)
        {
            var activity = _actRepository.GetById(id);
            
            if(activity == null)
            {
                throw new Exception();
            }

            _actRepository.Delete(activity);
        }

        public RunActivity GetActivity(int id)
        {
            return _actRepository.GetById(id) ?? null;
        }

        public IEnumerable<RunActivity> GetAllActivities()
        {
            return _actRepository.GetAll();
        }

        public void UpdateActivity(RunActivity activity)
        {
            // Calculate the Duration and AveragePace
            activity.Duration = activity.DateTimeEnded - activity.DateTimeStarted;

            double paceSecondsPerKm = activity.Duration.TotalSeconds / (double)activity.Distance;
            activity.AveragePace = TimeSpan.FromSeconds(paceSecondsPerKm);

            _actRepository.Update(activity);
        }
    }
}