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
        public async Task<ActionResult<ApiResponse<List<UserResponseDTO>>>> Get()
        {
            var users = await _userService.GetUsersAsync();
            _logger.LogInformation("Fetched all users");
            return Ok(ApiResponse<List<UserResponseDTO>>.Ok(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound(ApiResponse<UserResponseDTO>.Fail(new() { new ErrorDTO { Message = $"User {id} not found" } }));
            }
            _logger.LogInformation("Fetched user {Id}", id);
            return Ok(ApiResponse<UserResponseDTO>.Ok(user));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> Post([FromBody] UserRequestDTO user)
        {
            try
            {
                var createdUser = await _userService.AddUserAsync(user);
                _logger.LogInformation("Created user {Id}", createdUser.UserId);
                return CreatedAtAction(nameof(Get), new { id = createdUser.UserId },
                    ApiResponse<UserResponseDTO>.Ok(createdUser));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Failed to create user: {Message}", ex.Message);
                return BadRequest(ApiResponse<UserResponseDTO>.Fail(new() { new ErrorDTO { Message = ex.Message } }));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDTO>>> Put(int id, [FromBody] UserResponseDTO user)
        {
            if (id != user.UserId)
            {
                _logger.LogWarning("ID mismatch: {Id} vs {UserId}", id, user.UserId);
                return BadRequest(ApiResponse<UserResponseDTO>.Fail(new() { new ErrorDTO { Message = "ID mismatch" } }));
            }
            var updatedUser = await _userService.UpdateUserAsync(user);
            if (updatedUser == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound(ApiResponse<UserResponseDTO>.Fail(new() { new ErrorDTO { Message = $"User {id} not found" } }));
            }
            _logger.LogInformation("Updated user {Id}", id);
            return Ok(ApiResponse<UserResponseDTO>.Ok(updatedUser));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser == null)
            {
                _logger.LogWarning("User {Id} not found", id);
                return NotFound(ApiResponse<object>.Fail(new() { new ErrorDTO { Message = $"User {id} not found" } }));
            }
            _logger.LogInformation("Deleted user {Id}", id);
            return Ok(ApiResponse<object>.Ok(null));
        }
    }
}