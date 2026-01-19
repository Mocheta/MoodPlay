using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Data;
using MoodPlay.API.DTOs;
using MoodPlay.API.Models;
using MoodPlay.API.Services.Interfaces;

namespace MoodPlay.API.Services
{
    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _context;

        public SessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SessionDto?> CreateSessionAsync(Guid userId, CreateSessionDto createSessionDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return null;

            var session = new UserSession
            {
                UserId = userId,
                Mode = createSessionDto.Mode.ToLower(),
                MoodId = createSessionDto.MoodId,
                CustomPrompt = createSessionDto.CustomPrompt,
                DrinkLevel = createSessionDto.DrinkLevel,
                StartedAt = DateTime.UtcNow
            };

            _context.UserSessions.Add(session);
            await _context.SaveChangesAsync();

            return await GetSessionByIdAsync(session.Id);
        }

        public async Task<SessionDto?> GetSessionByIdAsync(Guid sessionId)
        {
            var session = await _context.UserSessions
                .Include(s => s.Mood)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                return null;

            var moodDto = session.Mood != null ? new MoodDto
            {
                Id = session.Mood.Id,
                Name = session.Mood.Name,
                Slug = session.Mood.Slug,
                Description = session.Mood.Description,
                IconName = session.Mood.IconName,
                ColorHex = session.Mood.ColorHex,
                Category = session.Mood.Category
            } : null;

            return new SessionDto
            {
                Id = session.Id,
                UserId = session.UserId,
                Mode = session.Mode,
                MoodId = session.MoodId,
                MoodName = moodDto?.Name,
                Mood = moodDto,
                CustomPrompt = session.CustomPrompt,
                DrinkLevel = session.DrinkLevel,
                StartedAt = session.StartedAt,
                EndedAt = session.EndedAt
            };
        }

        public async Task<List<SessionDto>> GetUserSessionsAsync(Guid userId)
        {
            return await _context.UserSessions
                .Where(s => s.UserId == userId)
                .Include(s => s.Mood)
                .OrderByDescending(s => s.StartedAt)
                .Select(s => new SessionDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Mode = s.Mode,
                    MoodId = s.MoodId,
                    MoodName = s.Mood != null ? s.Mood.Name : null,
                    Mood = s.Mood != null ? new MoodDto
                    {
                        Id = s.Mood.Id,
                        Name = s.Mood.Name,
                        Slug = s.Mood.Slug,
                        ColorHex = s.Mood.ColorHex,
                        Category = s.Mood.Category
                    } : null,
                    CustomPrompt = s.CustomPrompt,
                    DrinkLevel = s.DrinkLevel,
                    StartedAt = s.StartedAt,
                    EndedAt = s.EndedAt
                })
                .ToListAsync();
        }

        public async Task<bool> EndSessionAsync(Guid sessionId)
        {
            var session = await _context.UserSessions.FindAsync(sessionId);
            if (session == null)
                return false;

            session.EndedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}