using Microsoft.AspNetCore.Mvc;
using RunTracker.API.Data;
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

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {  
            try
            {
                _userService.AddUser(user);
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user");
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
                _logger.LogError(ex, "Error occurred while retrieving all users");
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
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch(Exception ex){
                _logger.LogError(ex, "Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user");
                return BadRequest(ex.Message);
            }
        }

        // Activity 
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
                _logger.LogError(ex, "Error occurred while retrieving activity");
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
                _logger.LogError(ex, "Error occurred while retrieving all activities");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("AddActivity")]
        public IActionResult AddActivity(RunActivity activity)
        {
            try
            {
                _runActivityService.AddActivity(activity);
                return Ok("Activity added successfully.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating activity");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("UpdateActivity")]
        public IActionResult UpdateActivity(RunActivity activity)
        {
            try
            {
                _runActivityService.UpdateActivity(activity);
                return Ok("User updated successfully.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating activity");
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("DeleteActivity/{id}")]
        public IActionResult DeleteActivity(int id)
        {
            try
            {
                _runActivityService.DeleteActivity(id);
                return Ok("Activity deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting activity");
                return BadRequest(ex.Message);
            }
        }
    }

}