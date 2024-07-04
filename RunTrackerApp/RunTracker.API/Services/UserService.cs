
using RunTracker.API.Data;
using RunTracker.API.Data.Repositories;

namespace RunTracker.API.Services{

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<User> userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public void AddUser(User user)
        {
            try
            {
                _logger.LogInformation($"Inserting User: {user.Name}");
                // Calculate the age and BMI
                DateTime currentDate = DateTime.Today;
                decimal heightM = user.Height / (decimal)100.00;

                if (user.BirthDate > currentDate)
                {
                    _logger.LogError($"{DateTime.Now}: Error occurred invalid BirthDate {nameof(AddUser)}");
                    throw new Exception("Error occurred invalid BirthDate.");
                }

                int age = currentDate.Year - user.BirthDate.Year;

                if (user.BirthDate.Month > currentDate.Month || (user.BirthDate.Month == currentDate.Month && user.BirthDate.Day > currentDate.Day))
                {
                    age--;
                }

                user.Age = age;
                user.Bmi = user.Weight / (heightM * heightM);

                _userRepository.Add(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(AddUser)}");
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);
                _logger.LogInformation($"Deleting User: {user.Name}");
                _userRepository.Delete(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(DeleteUser)}");
                throw;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                _logger.LogInformation($"Retrieving All User");
                return _userRepository.GetAll();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(GetAllUsers)}");
                throw;
            }
        }

        public User? GetUser(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving User: {id}");
                return _userRepository.GetById(id) ?? null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(GetUser)}");
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _logger.LogInformation($"Updating User: {user.Name}");
                 // Calculate the age and BMI
                DateTime currentDate = DateTime.Today;
                decimal heightM = user.Height / (decimal)100.00;

                if (user.BirthDate > currentDate)
                {
                    _logger.LogError($"{DateTime.Now}: Error occurred invalid BirthDate {nameof(UpdateUser)}");
                    throw new Exception("Error occurred invalid BirthDate.");
                }

                int age = currentDate.Year - user.BirthDate.Year;

                if (user.BirthDate.Month > currentDate.Month || (user.BirthDate.Month == currentDate.Month && user.BirthDate.Day > currentDate.Day))
                {
                    age--;
                }

                user.Age = age;
                user.Bmi = user.Weight / (heightM * heightM);

                _userRepository.Update(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred in {nameof(UpdateUser)}");
                throw;
            }
        }
    }
}
