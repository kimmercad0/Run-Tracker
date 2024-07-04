using RunTracker.API.Data;

namespace RunTracker.API.Services{

    public interface IRunActivityService
    {
        RunActivity GetActivity(int id);
        IEnumerable<RunActivity> GetAllActivities();
        void AddActivity(RunActivity activity);
        void UpdateActivity(RunActivity activity);
        void DeleteActivity(int id);
    }
}
