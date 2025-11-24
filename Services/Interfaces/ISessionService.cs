using MoodPlay.API.DTOs;
using MoodPlay.API.Models;

namespace MoodPlay.API.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionDto?> CreateSessionAsync(Guid userId, CreateSessionDto createSessionDto);
        Task<SessionDto?> GetSessionByIdAsync(Guid sessionId);
        Task<List<SessionDto>> GetUserSessionsAsync(Guid userId);
        Task<bool> EndSessionAsync(Guid sessionId);
    }
}
