
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
            // Calculate the age and BMI
            DateTime currentDate = DateTime.Today;
            decimal heightM = user.Height / (decimal)100.00;

            if (user.BirthDate > currentDate)
            {
                throw new Exception();
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

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            
            if(user == null)
            {
                throw new Exception();
            }

            _userRepository.Delete(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User? GetUser(int id)
        {
            return _userRepository.GetById(id) ?? null;
        }

        public void UpdateUser(User user)
        {
            // Calculate the age and BMI
            DateTime currentDate = DateTime.Today;
            decimal heightM = user.Height / (decimal)100.00;

            if (user.BirthDate > currentDate)
            {
                throw new Exception();
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
    }
}
