using Backend.DTO;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var users = await _userService.GetUsersAsync();
            _logger.LogInformation("Fetched all users");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Fetched user {Id}", id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO user)
        {
            var createdUser = await _userService.AddUserAsync(user);
            _logger.LogInformation("Created user {Id}", createdUser.UserId);
            return CreatedAtAction(nameof(Get), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserDTO user)
        {
            if (id != user.UserId)
            {
                _logger.LogWarning("ID mismatch: {Id} vs {UserId}", id, user.UserId);
                return BadRequest();
            }
            var updatedUser = await _userService.UpdateUserAsync(user);
            if (updatedUser == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Updated user {Id}", id);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Deleted user {Id}", id);
            return NoContent();
        }
    }
}
