using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionResponseDTO> CreateSessionAsync(SessionRequestDTO session);
        Task<SessionResponseDTO> GetSessionAsync(Guid sessionId);
        Task<List<SessionResponseDTO>> GetSessionsByUserAsync(int userId);
        Task<SessionResponseDTO> ValidateSessionAsync(Guid sessionId);
        Task DeleteSessionAsync(Guid sessionId);
    }
}
