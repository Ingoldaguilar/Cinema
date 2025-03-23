using Backend.DTO;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
        {
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SessionResponseDTO>>> Post([FromBody] SessionRequestDTO session)
        {
            var createdSession = await _sessionService.CreateSessionAsync(session);
            _logger.LogInformation("Created session {Id}", createdSession.SessionId);
            return CreatedAtAction(nameof(GetSession), new { sessionId = createdSession.SessionId },
                ApiResponse<SessionResponseDTO>.Ok(createdSession));
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<ApiResponse<SessionResponseDTO>>> GetSession(Guid sessionId)
        {
            var session = await _sessionService.GetSessionAsync(sessionId);
            if (session == null)
            {
                _logger.LogWarning("Session {Id} not found", sessionId);
                return NotFound(ApiResponse<SessionResponseDTO>.Fail(new() { new ErrorDTO { Message = $"Session {sessionId} not found" } }));
            }
            _logger.LogInformation("Fetched session {Id}", sessionId);
            return Ok(ApiResponse<SessionResponseDTO>.Ok(session));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<SessionResponseDTO>>>> GetSessionsByUser(int userId)
        {
            var sessions = await _sessionService.GetSessionsByUserAsync(userId);
            _logger.LogInformation("Fetched sessions for user {Id}", userId);
            return Ok(ApiResponse<List<SessionResponseDTO>>.Ok(sessions));
        }

        [HttpGet("validate/{sessionId}")]
        public async Task<ActionResult<ApiResponse<SessionResponseDTO>>> ValidateSession(Guid sessionId)
        {
            var session = await _sessionService.ValidateSessionAsync(sessionId);
            if (session == null)
            {
                _logger.LogWarning("Session {Id} invalid or expired", sessionId);
                return NotFound(ApiResponse<SessionResponseDTO>.Fail(new() { new ErrorDTO { Message = $"Session {sessionId} invalid or expired" } }));
            }
            _logger.LogInformation("Validated session {Id}", sessionId);
            return Ok(ApiResponse<SessionResponseDTO>.Ok(session));
        }

        [HttpDelete("{sessionId}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(Guid sessionId)
        {
            await _sessionService.DeleteSessionAsync(sessionId);
            var session = await _sessionService.GetSessionAsync(sessionId); // Check if gone
            if (session != null)
            {
                _logger.LogWarning("Failed to delete session {Id}", sessionId);
                return BadRequest(ApiResponse<object>.Fail(new() { new ErrorDTO { Message = $"Failed to delete session {sessionId}" } }));
            }
            _logger.LogInformation("Deleted session {Id}", sessionId);
            return Ok(ApiResponse<object>.Ok(null));
        }
    }
}