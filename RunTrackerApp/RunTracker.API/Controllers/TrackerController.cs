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
            _logger.LogInformation("GetUser action called with ID: {UserId}", id);
            try
            {
                _userService.AddUser(user);
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user with ID: {UserId}", id);
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
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
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
                return BadRequest(ex.Message);
            }
        }

        // Activity 
        [HttpGet("GetActivity/{id}")]
        public IActionResult GetActivity(int id)
        {
            var activity = _runActivityService.GetActivity(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpGet("GetAllActivities")]
        public IActionResult GetAllActivities()
        {
            var activities = _runActivityService.GetAllActivities();
            return Ok(activities);
        }

        [HttpPost("AddActivity")]
        public IActionResult AddActivity(RunActivity activity)
        {
            _runActivityService.AddActivity(activity);
            return Ok("Activity added successfully.");
        }

        [HttpPut("UpdateActivity")]
        public IActionResult UpdateActivity(RunActivity activity)
        {
            _runActivityService.UpdateActivity(activity);
           return Ok("User updated successfully.");
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
                return NotFound(ex.Message);
            }
        }
    }

}