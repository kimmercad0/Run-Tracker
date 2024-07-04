
using RunTracker.API.Models;

namespace RunTracker.API.Data.Repositories{

    public class UserRepository : IRepository<User>
    {
        private readonly RunTrackerDbContext _context;

        public UserRepository(RunTrackerDbContext context)
        {
            _context = context;
        }
        
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}