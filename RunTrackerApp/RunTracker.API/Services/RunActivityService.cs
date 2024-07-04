
using RunTracker.API.Models;
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
            try
            {
                _logger.LogInformation($"Inserting Activity");
                // Calculate the Duration and AveragePace
                activity.Duration = activity.DateTimeEnded - activity.DateTimeStarted;
                activity.AveragePace = TimeSpan.FromTicks(activity.Duration.Ticks / (long)activity.Distance);

                _actRepository.Add(activity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(AddActivity)}");
                throw;
            }
        }

        public void DeleteActivity(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting Activity ID: '{id}'");
                var activity = _actRepository.GetById(id);
                _actRepository.Delete(activity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(DeleteActivity)}");
                throw;
            }
        }

        public RunActivity GetActivity(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Activity ID: '{id}'");
                return _actRepository.GetById(id) ?? null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(GetActivity)}");
                throw;
            }
           
        }

        public IEnumerable<RunActivity> GetAllActivities()
        {
            try
            {
                _logger.LogInformation($"Retrieving All Activities");
                return _actRepository.GetAll();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(GetAllActivities)}");
                throw;
            }
           
        }

        public void UpdateActivity(RunActivity activity)
        {
            try
            {
                _logger.LogInformation($"Updating Activity ID: '{activity.RunId}'");
                // Calculate the Duration and AveragePace
                activity.Duration = activity.DateTimeEnded - activity.DateTimeStarted;
                activity.AveragePace = TimeSpan.FromTicks(activity.Duration.Ticks / (long)activity.Distance);

                _actRepository.Update(activity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(UpdateActivity)}");
                throw;
            }
        }
    }
}