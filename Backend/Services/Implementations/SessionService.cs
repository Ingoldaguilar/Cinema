using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace Backend.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly IWorkUnit _workUnit;
        private readonly ILogger<SessionService> _logger;

        public SessionService(IWorkUnit workUnit, ILogger<SessionService> logger)
        {
            _workUnit = workUnit ?? throw new ArgumentNullException(nameof(workUnit));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private SessionResponseDTO ToResponse(Session session) => session == null ? null : new SessionResponseDTO
        {
            SessionId = session.SessionId,
        };

        private Session FromRequest(SessionRequestDTO session) => new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = session.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(24) // Adjust expiry as needed
        };

        public async Task<SessionResponseDTO> CreateSessionAsync(SessionRequestDTO session)
        {
            var sessionEntity = FromRequest(session);
            await _workUnit.SessionDAL.AddAsync(sessionEntity);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("Session {Id} created for user {UserId}", sessionEntity.SessionId, session.UserId);
            return ToResponse(sessionEntity);
        }

        public async Task<SessionResponseDTO> GetSessionAsync(Guid sessionId)
        {
            var session = await _workUnit.SessionDAL.GetByIdAsync(sessionId);
            return ToResponse(session);
        }

        public async Task<List<SessionResponseDTO>> GetSessionsByUserAsync(int userId)
        {
            var sessions = await _workUnit.SessionDAL.GetByUserIdAsync(userId);
            return sessions.Select(ToResponse).ToList();
        }

        public async Task<SessionResponseDTO> ValidateSessionAsync(Guid sessionId)
        {
            var session = await _workUnit.SessionDAL.GetValidSessionAsync(sessionId);
            return ToResponse(session);
        }

        public async Task DeleteSessionAsync(Guid sessionId)
        {
            var session = await _workUnit.SessionDAL.GetByIdAsync(sessionId);
            if (session == null)
            {
                _logger.LogWarning("Session {Id} not found", sessionId);
                return;
            }
            await _workUnit.SessionDAL.DeleteAsync(sessionId);
            await _workUnit.CompleteAsync();
            _logger.LogInformation("Session {Id} deleted", sessionId);
        }
    }
}