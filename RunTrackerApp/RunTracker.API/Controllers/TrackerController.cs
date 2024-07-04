using Microsoft.AspNetCore.Mvc;
using RunTracker.API.Models;
using RunTracker.API.Services;

namespace RunTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackerController : ControllerBase
    {

        private readonly ILogger<TrackerController> _logger;
        private readonly IUserService _userService;
        private readonly IRunActivityService _runActivityService;

        public TrackerController(IUserService userService, IRunActivityService runActivityService, ILogger<TrackerController> logger)
        {
            _userService = userService;
            _runActivityService = runActivityService;
            _logger = logger;
        }

        #region User
        // User Section
        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {  
            try
            {
                _userService.AddUser(user);
                return Ok($"User '{user.Name}' added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {   
                var user = _userService.GetUser(id);
                _userService.DeleteUser(id);
                
                return Ok($"User '{user.Name}' deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while deleting user");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
           try 
           {
                var users = _userService.GetAllUsers();
                return Ok(users);
           }
           catch(Exception ex)
           {
                _logger.LogError(ex, $"{DateTime.Now}: occurred while retrieving all users");
                return BadRequest(ex.Message);
           }
        }

        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);

                if (user == null)
                {
                    return NotFound($"User '{user.Name}' not found.");
                }

                return Ok(user);
            }
            catch(Exception ex){
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok($"User '{user.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }
        #endregion User

        #region Activity
        // Activity Section
        [HttpGet("GetActivity/{id}")]
        public IActionResult GetActivity(int id)
        {
            try
            {
                var activity = _runActivityService.GetActivity(id);
                if (activity == null)
                {
                    return NotFound();
                }
                return Ok(activity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while retrieving activity");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("GetAllActivities")]
        public IActionResult GetAllActivities()
        {
            try
            {
                var activities = _runActivityService.GetAllActivities();
                return Ok(activities);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while retrieving all activities");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("AddActivity")]
        public IActionResult AddActivity(RunActivity activity)
        {
            try
            {
                _runActivityService.AddActivity(activity);
                var user = _userService.GetUser(activity.UserId.Value);
                return Ok($"Activity added successfully to '{user.Name}'.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while creating activity");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("UpdateActivity")]
        public IActionResult UpdateActivity(RunActivity activity)
        {
            try
            {
                _runActivityService.UpdateActivity(activity);
                var user = _userService.GetUser(activity.UserId.Value);
                return Ok($"Activity updated successfully to '{user.Name}'.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while updating activity");
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("DeleteActivity/{id}")]
        public IActionResult DeleteActivity(int id)
        {
            try
            {
                _runActivityService.DeleteActivity(id);
                return Ok($"Activity ID '{id}' is deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error occurred while deleting activity");
                return BadRequest(ex.Message);
            }
        }
        #endregion Activity
    }

}